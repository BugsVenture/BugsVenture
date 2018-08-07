using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestLoadLevel2 : MonoBehaviour
{
    public string level2;
    public GameObject InteractUI;
    // Use this for initialization
    void Start ()
    {
        InteractUI.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider other)
    {
       if(other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E) || other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            SceneManager.LoadScene(level2);
        }
        if (other.gameObject.tag == "Player")
        {
            InteractUI.SetActive(true);

        }
    }

    void OnTriggerExit(Collider other)
    {
            InteractUI.SetActive(false); 
    }
}
