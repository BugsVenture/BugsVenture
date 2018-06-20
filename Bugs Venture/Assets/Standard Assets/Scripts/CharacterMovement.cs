using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;



public class CharacterMovement : MonoBehaviour
{
    // Public
    public float moveSpeed;
    public float speed = 10f;
    public float rotationSpeed = 10f;
    public float rotatespeed;
    public bool useController;
    public GameObject PulsEffect;
    public GameObject shield;
    public int randomMin;
    public int randomMax;
    public float shieldDuration = 5f;
    public Vector3 ResetPoint;
    public float maxDistance = 5f;

    // Private
    private bool isAttacking = true;
    private Rigidbody RigidBody;
    private Camera MainCamera;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private float stamina = 1, maxStamina = 1;
    private BarScript bar;
    private int number;

    // Use this for initialization
    void Start()
    {
        ResetPoint = new Vector3(1, 1, 1);
        number = Random.Range(randomMin, randomMax);
        print(number);
        bar = GameObject.FindObjectOfType<BarScript>();
        RigidBody = GetComponent<Rigidbody>();
        MainCamera = FindObjectOfType<Camera>();
        bar.fillAmount = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {

        //Player Movement
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * moveSpeed;



        //Random Attack Keyboard or Controller
        if (Input.GetKeyDown(GameManager.GM.teleportKey) && isAttacking && number <= 49|| 
            Input.GetKeyDown(KeyCode.Joystick1Button4) && isAttacking && number <= 49)
        {
            Teleport();
        }
        else if(Input.GetKeyDown(GameManager.GM.teleportKey) && isAttacking && number >= 50||
            Input.GetKeyDown(KeyCode.Joystick1Button4) && isAttacking && number >= 50)
        {
            Shield();
        }

        // Attackcheck
        if (bar.fillAmount == 0 && isAttacking)
        {
            bar.fillAmount += Time.deltaTime;
            StartCoroutine(RegenerateDelay());

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
        {
            Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("HorizontalJ") + Vector3.forward * Input.GetAxisRaw("VerticalJ");
                if(playerDirection.sqrMagnitude > 0.4f)
                {
                    transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                }
        }
    }

    void FixedUpdate()
    {
        RigidBody.velocity = moveVelocity;
        GameObject.Find("Shield(Clone)").transform.position = this.transform.position;
    }


    //Teleport forward function
    public void Teleport()
    {
        bar.fillAmount -= 1f;
        Vector3 ray = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        
        if(Physics.Raycast(this.transform.position, ray, out hit, maxDistance))
        {
            this.transform.position = hit.point -= this.transform.forward * 1;
        }
        else
        {
            this.transform.position += this.transform.forward * 10;
        }

        number = Random.Range(randomMin, randomMax);
    }

    // Puls Attacke function
    public void PulsAttack()
    {
        bar.fillAmount -= 1f;
        Instantiate(PulsEffect, this.transform.position, Quaternion.Euler(new Vector3(90, 0, 0)));
        number = Random.Range(randomMin, randomMax);
    }

    //Shield function
    public void Shield()
    {
        bar.fillAmount -= 1f;
        GameObject inst = Instantiate(shield, this.transform.position, Quaternion.identity);
        Destroy(inst,shieldDuration);
        number = Random.Range(randomMin, randomMax);
    }

    // regenerate Delay for Teleportstamina
    IEnumerator RegenerateDelay()
    {
        isAttacking = false;
        yield return new WaitForSeconds(6);
        isAttacking = true;
    }

    //Reset player 
    void OnTriggerEnter(Collider other)
    {
    if(other.gameObject.tag == "Outside")
        {
            this.transform.position = ResetPoint;
        }
    }

}
