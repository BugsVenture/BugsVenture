using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;



public class CharacterMovement : MonoBehaviour
{
    // Public
    public float moveSpeed;
    public float speed = 10f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float rotationSpeed = 10f;
    public float rotatespeed;
    public bool useController;
    public Transform Teleportpoint;
    public GameObject PulsEffect;
    public GameObject shield;
    public int randomMin;
    public int randomMax;
    //public float maxHealth;

    // Private
    private bool isAttacking = true;
    private Rigidbody RigidBody;
    private Camera MainCamera;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private float stamina = 1, maxStamina = 1;
    private BarScript bar;
    private GameObject ResetPoint;
    private int number;
    //private float damage = 0.1f;

    // Use this for initialization
    void Start()
    {
        number = Random.Range(randomMin, randomMax);
        print(number);
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



        //Random Attack Keyboard
        if (Input.GetKeyDown(GameManager.GM.teleportKey) || Input.GetKeyDown(KeyCode.Joystick1Button3) && isAttacking && number <= 40) //not very beautiful code 
        {
            Teleport();
            bar.fillAmount -= 1f;
        }
        else if(Input.GetKeyDown(GameManager.GM.teleportKey) && isAttacking && number == Random.Range(40, 50))
        {
            PulsAttack();
            bar.fillAmount -= 1f;
        }
        else if(Input.GetKeyDown(GameManager.GM.teleportKey) && isAttacking && number >= 50)
        {
            Shield();
            bar.fillAmount -= 1f;
        }

        //Random Attack JoyStick
        if (Input.GetKeyDown(KeyCode.Joystick1Button3) && isAttacking && number <= 40) // TODO: Try to use a function here. Avoid write the same code
        {
            Teleport();
            bar.fillAmount -= 1f;
        }
        else if(Input.GetKeyDown(KeyCode.Joystick1Button3) && isAttacking && number == Random.Range(40, 50))
        {
            PulsAttack();
            bar.fillAmount -= 1f;
        }
        else if(Input.GetKeyDown(KeyCode.Joystick1Button3) && isAttacking && number >= 50)
        {
            Shield();
            bar.fillAmount -= 1f;
        }

        // Attackcheck
        if (bar.fillAmount <= 0f)
        {
            isAttacking = false;
            StartCoroutine(RegenerateDelay());
        }

        if (bar.fillAmount == maxStamina)
        {
            isAttacking = true;
        }

        //Rotate Player with Mouse
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
            return;
        }

        //Rotate Player with Controller
        //useController // TODO: unnecessary
        {
            Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("HorizontalJ") + Vector3.forward * Input.GetAxisRaw("VerticalJ");
            if (playerDirection.sqrMagnitude > 0.0f)
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
        number = Random.Range(randomMin, randomMax);
    }

    // Puls Attacke function
    public void PulsAttack()
    {
        Instantiate(PulsEffect, this.transform.position, Quaternion.Euler(new Vector3(90, 0, 0)));
        number = Random.Range(randomMin, randomMax);
    }

    //Shield function
    public void Shield()
    {
        GameObject inst = Instantiate(shield, this.transform.position, Quaternion.identity); //TODO: returns the instantiated gameobject, which u can use below
        Destroy(inst, 5); //TODO: try to avoid hardcoded variables
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
        isAttacking = false;
        yield return new WaitForSeconds(5);
        bar.fillAmount = 1f;
        isAttacking = false;
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
