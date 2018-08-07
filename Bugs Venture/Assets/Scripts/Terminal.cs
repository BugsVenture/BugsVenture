using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    public GameObject Door;
    public GameObject Lamp;
    public float timeBevorActivate = 1f;
    public GameObject InteractUI;
    public GameObject ExitTrigger;


     void Start()
    {
        InteractUI.SetActive(false);
        ExitTrigger.SetActive(false);
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
        InteractUI.SetActive(true);

        }
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            InteractUI.SetActive(false);
            StartCoroutine(LampActiveDelay());
            DoorOpen openCS = Door.GetComponent<DoorOpen>();
            openCS.IncrementCount();
            Destroy(GetComponent<BoxCollider>());
        }

        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            InteractUI.SetActive(false);
            StartCoroutine(LampActiveDelay());
            DoorOpen openCS = Door.GetComponent<DoorOpen>();
            openCS.IncrementCount();
            Destroy(GetComponent<BoxCollider>());
        }

    }

    private void OnTriggerExit(Collider other)
    {

            InteractUI.SetActive(false);

    }



    IEnumerator LampActiveDelay()
    {
        yield return new WaitForSeconds(timeBevorActivate);
        Lamp.SetActive(false);
        ExitTrigger.SetActive(true);
    }
}
