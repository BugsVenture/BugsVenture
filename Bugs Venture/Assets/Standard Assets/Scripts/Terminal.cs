using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    //Public
    public GameObject TerminalTrigger;

    //Private
    private OpenExitDoor openExitDoor;

    void Start()
    {
        openExitDoor = GameObject.FindObjectOfType<OpenExitDoor>();  
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            openExitDoor.terminalCount++;
            Destroy(TerminalTrigger.gameObject);
        }

        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            openExitDoor.terminalCount++;
            Destroy(TerminalTrigger.gameObject);
        }

    }
}
