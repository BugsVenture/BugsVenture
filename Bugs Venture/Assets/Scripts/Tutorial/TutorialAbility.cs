using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAbility : MonoBehaviour
{

    //Public
    public GameObject TutUiAbility;

    // Use this for initialization
    void Start()
    {
        TutUiAbility.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(GameManager.GM.teleportKey)||
           Input.GetKeyDown(KeyCode.Joystick1Button4))
        { 
            Destroy(this.TutUiAbility);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            TutUiAbility.SetActive(true);
        }
    }
}
