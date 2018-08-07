using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    //Public
    public float minWaitTime = 0.001f;
    public float maxWaitTime = 0.2f;
    private bool isEnabled = false;
    private AudioSource aSource; 


	// Use this for initialization
	void Start ()
    {
        aSource = GetComponent<AudioSource>();
        isEnabled = this.GetComponent<Light>().enabled; 
        StartCoroutine(LightFlick());
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(isEnabled)
        {
            aSource.Play();
            return;
        }
        aSource.Stop();
    }

    IEnumerator LightFlick()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            isEnabled = !isEnabled;
            this.GetComponent<Light>().enabled = isEnabled;
        }
    }
}
