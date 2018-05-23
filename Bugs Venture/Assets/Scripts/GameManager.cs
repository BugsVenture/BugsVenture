using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Public
    public static GameManager GM;
    public KeyCode teleportKey { get; set;}
    public int leverCount;
    public int leverCountMax = 2;
    public GameObject Door;


    void Awake()
    {
        if(GM == null)
        {
            DontDestroyOnLoad(gameObject);
            GM = this;
        }
        else if(GM != this)
        {
            Destroy(gameObject);
        }

        teleportKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("TeleportKey"));
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (leverCount == leverCountMax)
        {
            Door.GetComponent<Animation>().Play();
            StartCoroutine(delay());
        }
    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(1);
        Door.GetComponent<Animation>().Stop();
    }
}
