using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    //Public
    public Transform Teleportpoint;

    //Private
    private static Teleporter playerInstance;
    private Transform target;

    private void Awake()
    {
        if (playerInstance == null)
            playerInstance = this;
    }
    public static Teleporter GetInstance()
    {
        return playerInstance;
    }
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            target = Player.GetInstance().transform;
            target.transform.position = Teleportpoint.transform.position;
        }
    }
}
