using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldLifeTime : MonoBehaviour {

    //Public
    public bool isFlickering = false;
    public float lifeTime;
    public float on = 0.2f;
    public float off = 1f;


     void Start()
    {
        StartCoroutine(LifeTime());
    }
    // Update is called once per frame
    void Update()
    {
        if (isFlickering)
        {
            ShieldFlicker();
        }
    }

    void ShieldFlicker()
    {
        StartCoroutine(On());
    }

    IEnumerator On()
    {
        yield return new WaitForSeconds(on);
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        StartCoroutine(Off());
    }

    IEnumerator Off()
    {
        yield return new WaitForSeconds(off);
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        StartCoroutine(On());
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        isFlickering = true;
    }
}
