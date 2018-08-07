using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    public Material[] material;
    Renderer rend;

	// Use this for initialization
	public void Start ()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
        BlinkMaterial();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   public void BlinkMaterial()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        rend.sharedMaterial = material[0];
        yield return new WaitForSeconds(0.2f);
        rend.sharedMaterial = material[1];
        yield return new WaitForSeconds(0.2f);
        rend.sharedMaterial = material[0];
    }
}
