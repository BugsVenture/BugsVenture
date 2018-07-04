using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    //Public
    public Transform target;
    public float spped = 1f;

    //Private
    private int randomTarget;
    private Quaternion newRot;
    private Vector3 relPos;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	if(Input.GetKeyDown(KeyCode.L))
        {
            transform.LookAt(target);
        }
        else
        {

        }
	}
}
