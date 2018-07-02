using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestLoadLevel2 : MonoBehaviour
{
    public string level2;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(level2);
        }
    }
}
