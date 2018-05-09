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
    public float rotatespeed;
    public bool useController;
    public Transform Teleportpoint;

    //public float maxHealth;

    //Private
    private bool isTeleporting = true;
    private Rigidbody RigidBody;
    private Camera MainCamera;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private float stamina = 1, maxStamina = 1;
    private BarScript bar;
    private GameObject ResetPoint;
    //private float damage = 0.1f;

    // Use this for initialization
    void Start()
    {
        ResetPoint = GameObject.Find("Resetpoint");
        bar = GameObject.FindObjectOfType<BarScript>();
        RigidBody = GetComponent<Rigidbody>();
        MainCamera = FindObjectOfType<Camera>();
        bar.fillAmount = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        //HealthBar

        //bar.fillAmount = maxHealth;

        ////Player Death
        //if (maxHealth == 0)
        //{
        //    Application.LoadLevel(Application.loadedLevel);
        //}
   
        //Player Movement
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * moveSpeed;


        //Teleport Mouse
        if (Input.GetKeyDown(GameManager.GM.teleportKey) && isTeleporting)
        {
            Teleport();
            bar.fillAmount -= 0.1f;
        }
        //Teleport JoyStick
        if (Input.GetKeyDown(KeyCode.Joystick1Button3) && isTeleporting)
        {
            Teleport();
            bar.fillAmount -= 0.1f;
        }

        // Teleportcheck
        if (bar.fillAmount <= 0f)
        {
            isTeleporting = false;
            StartCoroutine(RegenerateDelay());
        }

        if (bar.fillAmount == maxStamina)
        {
            isTeleporting = true;
        }

        //Rotate with Mouse
        if (!useController)
        {
            Ray cameraRay = MainCamera.ScreenPointToRay(Input.mousePosition);
        Plane GroundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLenght;

        if (GroundPlane.Raycast(cameraRay, out rayLenght))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLenght);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }

        }
        //Rotate with Controller
        if (useController)
        {
            Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("HorizontalJ") + Vector3.forward * Input.GetAxisRaw("VerticalJ");
                if(playerDirection.sqrMagnitude > 0.0f)
                {
                    transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                }
        }
    }

    void FixedUpdate()
    {
        RigidBody.velocity = moveVelocity;
    }


    //Teleport forward function
    public void Teleport()
    {
        this.transform.position = Teleportpoint.transform.position;
    }

    // Delay for Ignor Wallcollider
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        Physics.IgnoreLayerCollision(9, 10, false);
    }

    // regenerate Delay for Teleportstamina
    IEnumerator RegenerateDelay()
    {
        yield return new WaitForSeconds(3);
        bar.fillAmount = 0.5f;
        isTeleporting = false;
        yield return new WaitForSeconds(5);
        bar.fillAmount = 1f;
        isTeleporting = false;
    }


    

    //Reset player 
     void OnTriggerEnter(Collider other)
    {
    if(other.gameObject.tag == "Outside")
        {
            this.transform.position = ResetPoint.transform.position;
        }
    }
}
