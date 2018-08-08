using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    //Public
    public GameObject TutUi;

    // Use this for initialization
    void Start ()
    {
        TutUi.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Destroy(this.TutUi);
        }
    }

     void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            TutUi.SetActive(true);
        }
    }
}
