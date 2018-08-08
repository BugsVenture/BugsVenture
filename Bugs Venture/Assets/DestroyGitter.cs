using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGitter : MonoBehaviour
{
    public bool destroy = false;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(destroy)
        {
            this.gameObject.SetActive(false);
        }
	}
}
