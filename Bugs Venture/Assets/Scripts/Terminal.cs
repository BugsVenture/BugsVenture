using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    public GameObject Door;
    public GameObject Lamp;
    public float timeBevorActivate = 1f;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(LampActiveDelay());
            DoorOpen openCS = Door.GetComponent<DoorOpen>();
            openCS.IncrementCount();
            Destroy(GetComponent<BoxCollider>());
        }

        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            StartCoroutine(LampActiveDelay());
            DoorOpen openCS = Door.GetComponent<DoorOpen>();
            openCS.IncrementCount();
            Destroy(GetComponent<BoxCollider>());
        }

    }



    IEnumerator LampActiveDelay()
    {
        yield return new WaitForSeconds(timeBevorActivate);
        Lamp.SetActive(false);
    }
}
