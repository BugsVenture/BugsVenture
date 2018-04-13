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
    public float rotationSpeed = 10f;
    public float distance = 5.0f;
    public float rotatespeed;

    //Private
    private Rigidbody RigidBody;
    private Camera MainCamera;
    private bool grounded = false;
    private float groundCheckRadius = 0.05f;
    private Collider[] groundCollisions;
    private Vector3 moveInput;
    private Vector3 moveVelocity;

    // Use this for initialization
    void Start ()
    {
        RigidBody = GetComponent<Rigidbody>();
        MainCamera = FindObjectOfType<Camera>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Rotate with right Joystick 
        transform.Rotate(Input.GetAxis("HorizontalJ") * Vector3.up * Time.deltaTime * rotatespeed);

        //Player Movement
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * moveSpeed;

        //Teleport Mouse
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Teleport();
            Physics.IgnoreLayerCollision(10, 11, true);
            StartCoroutine(Delay());
        }

        //Teleport JoyStick
        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            Teleport();
            Physics.IgnoreLayerCollision(10, 11, true);
            StartCoroutine(Delay());
        }


        //Jump
        if (grounded && Input.GetButton("Jump"))
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
        RigidBody.velocity = moveVelocity;
    }

    //Teleport forward function
    public void Teleport()
    {

        RaycastHit hit;
        Vector3 destination = transform.position + transform.forward * distance;

        //obstacle found to be intersecting
        if (Physics.Linecast(transform.position, destination, out hit))
        {
            destination = transform.position + transform.forward * (hit.distance - 1f);
        }
        //no obstacles found
        if (Physics.Raycast(destination, -Vector3.up, out hit))
        {
            destination = hit.point;
            destination.y = 0.5f;
            transform.position = destination;
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        Physics.IgnoreLayerCollision(10, 11, false);
    }
}
