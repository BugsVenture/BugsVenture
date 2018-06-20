using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    //Public
    public float minWaitTime = 0.6f;
    public float maxWaitTime = 2f;


	// Use this for initialization
	void Start ()
    {
        StartCoroutine(LightFlick());
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    IEnumerator LightFlick()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            this.GetComponent<Light>().enabled = !this.GetComponent<Light>().enabled;
        }
    }
}
