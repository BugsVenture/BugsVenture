using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    //Public
    public GameObject Door;


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            this.Door.GetComponent<Animation>().Play();
        }

        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            this.Door.GetComponent<Animation>().Play();
        }

    }

}
