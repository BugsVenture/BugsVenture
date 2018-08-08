using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    public GameObject Door;
    public GameObject Lamp;
    public float timeBevorActivate = 1f;
    public GameObject InteractUi;



     void Start()
    {
        InteractUi.SetActive(false);
    }

    void OnTriggerStay(Collider other)
    {
        InteractUi.SetActive(true);

        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(LampActiveDelay());
            DoorOpen openCS = Door.GetComponent<DoorOpen>();
            openCS.IncrementCount();
            Destroy(GetComponent<BoxCollider>());
            Destroy(InteractUi);
        }

        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            StartCoroutine(LampActiveDelay());
            DoorOpen openCS = Door.GetComponent<DoorOpen>();
            openCS.IncrementCount();
            Destroy(GetComponent<BoxCollider>());
            Destroy(InteractUi);
        }

    }



    IEnumerator LampActiveDelay()
    {
        yield return new WaitForSeconds(timeBevorActivate);
        Lamp.SetActive(false);
    }
}
