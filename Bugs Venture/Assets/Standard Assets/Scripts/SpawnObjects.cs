using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    //Public
    public GameObject cube;

    //Private
    GameObject cubeclone;

	// Use this for initialization
	void Start ()
    {
        cubeclone = Instantiate(cube,transform.position,Quaternion.identity)as GameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
