using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterMovement))]
public class Player : MonoBehaviour {

    private static Player playerInstance;


    public static Player GetInstance()
    {
        return playerInstance;
    }

    private void Awake()
    {
        if(playerInstance == null)
            playerInstance = this;
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
