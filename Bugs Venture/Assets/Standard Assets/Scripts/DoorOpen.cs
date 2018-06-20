using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    //Public
    public GameObject Door;


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player"||other.gameObject.tag == "Enemy" )
        {
            this.Door.GetComponent<Animation>().Play();
        }


    }

}
