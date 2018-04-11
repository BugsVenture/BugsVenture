using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInstance : MonoBehaviour {

    private LevelInstance lvlInstance;

    public GameObject player;


    private void Awake()
    {
        if (lvlInstance == null)
            lvlInstance = this;
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (lvlInstance)
        {
            if (!Player.GetInstance() && SpawnPlayer.GetInstance())
            {
                SpawnPlayer.GetInstance().Spawn(player);
            }
        }
    }
}
