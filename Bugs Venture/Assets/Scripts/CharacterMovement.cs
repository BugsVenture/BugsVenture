using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharacterMovement : MonoBehaviour
{
    //Public
    public float moveSpeed;
    public float speed = 10f;
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
    private bool isTeleporting = true;
    private float stamina = 50, maxStamina = 50;
    Rect staminaRect;
    Texture2D staminaTexture;



    // Use this for initialization
    void Start()
    {
        staminaRect = new Rect(Screen.width / 30, Screen.height * 9 /10, Screen.width / 3, Screen.height / 50);
        staminaTexture = new Texture2D(1, 1);
        staminaTexture.SetPixel(0, 0, Color.blue);
        staminaTexture.Apply();
        RigidBody = GetComponent<Rigidbody>();
        MainCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {


        //Rotate with right Joystick 
        transform.Rotate(Input.GetAxis("HorizontalJ") * Vector3.up * Time.deltaTime * rotatespeed);

        //Player Movement
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * moveSpeed;


        //Teleport Mouse
        if (Input.GetKeyDown(KeyCode.Mouse1) && isTeleporting)
        {
            Teleport();
            Physics.IgnoreLayerCollision(10, 11, true);
            StartCoroutine(Delay());
            stamina -= 10f;
        }
        //Teleport JoyStick
        if (Input.GetKeyDown(KeyCode.Joystick1Button3) && isTeleporting)
        {
            Teleport();
            Physics.IgnoreLayerCollision(10, 11, true);
            StartCoroutine(Delay());
        }

        // Teleportcheck
        if(stamina == 0)
        {
            isTeleporting = false;
            StartCoroutine(RegenerateDelay());
        }

        if (stamina == maxStamina)
        {
            isTeleporting = true;
        }


        Ray cameraRay = MainCamera.ScreenPointToRay(Input.mousePosition);
        Plane GroundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLenght;

        if (GroundPlane.Raycast(cameraRay, out rayLenght))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLenght);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
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
    private void OnGUI()
    {
        float ratio = stamina / maxStamina;
        float rectWidth = ratio*Screen.width / 3;
        staminaRect.width = rectWidth;
        GUI.DrawTexture(staminaRect, staminaTexture);
    }

    // Delay for Ignor Wallcollider
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        Physics.IgnoreLayerCollision(10, 11, false);
    }

   // regenerate Delay for Teleportstamina
   IEnumerator RegenerateDelay()
    {
        yield return new WaitForSeconds(3);
        stamina = 25f;
        isTeleporting = false;
        yield return new WaitForSeconds(5);
        stamina = 50f;
        isTeleporting = false;
    }





}
