using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    public GameObject Door;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            DoorOpen openCS = Door.GetComponent<DoorOpen>();
            openCS.IncrementCount();
            Destroy(GetComponent<BoxCollider>());
        }

        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            DoorOpen openCS = Door.GetComponent<DoorOpen>();
            openCS.IncrementCount();
            Destroy(GetComponent<BoxCollider>());
        }

    }
}
