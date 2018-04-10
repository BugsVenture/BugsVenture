using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    //Public
    public float moveSpeed;
    public float speed = 10f;
    public float jumpHeight = 20f;
    public LayerMask groundLayer;
    public Transform groundCheck;

    //Private
    private Rigidbody RigidBody;
    private Camera MainCamera;
    private bool grounded = false;
    private float groundCheckRadius = 0.05f;
    private Collider[] groundCollisions;

    // Use this for initialization
    void Start ()
    {
        RigidBody = GetComponent<Rigidbody>();
        MainCamera = FindObjectOfType<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
    { 
        //Jump
        if ( grounded && Input.GetKey(KeyCode.Space))
        {          
            grounded = false;
            RigidBody.AddForce(new Vector3(0, jumpHeight, 0));
        }

        //Groundcheck
        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if (groundCollisions.Length > 0)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }    

        Ray cameraRay = MainCamera.ScreenPointToRay(Input.mousePosition);
        Plane GroundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLenght;

        if(GroundPlane.Raycast(cameraRay, out rayLenght))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLenght);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y,pointToLook.z));
        }
    }

   void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        RigidBody.velocity = new Vector3(move * moveSpeed, RigidBody.velocity.y, Input.GetAxis("Vertical") * moveSpeed);
    }
}
