using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointUi : MonoBehaviour
{

    public GameObject checkpointUi;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(delay());
	}
	
    IEnumerator delay()
    {
        yield return new WaitForSeconds(1);
        checkpointUi.SetActive(false);
    }
}
