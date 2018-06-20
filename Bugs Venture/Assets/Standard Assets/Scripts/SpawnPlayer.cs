using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{

    private static SpawnPlayer spawnInstance;


    private void Awake()
    {
        spawnInstance = this;
    }

    public static SpawnPlayer GetInstance()
    {
        return spawnInstance;
    }

    public void Spawn(GameObject playerObj)
    {
        GameObject playerObject = (GameObject)Instantiate(playerObj, transform.position, transform.rotation);        
        Player player = playerObject.AddComponent<Player>() as Player;
        CameraFollow camera = CameraFollow.GetInstance();
        camera = playerObject.AddComponent<CameraFollow>() as CameraFollow;
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
