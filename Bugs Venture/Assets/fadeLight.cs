using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeLight : MonoBehaviour
{
    public Light light;

	// Use this for initialization
	void Start () {
        light = GetComponent<Light>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        FadeLight();
	}

    void FadeLight()
    {
       while (light.intensity > 0.0f)
        {
            light.intensity -= 0.05f;
        }
       while (light.intensity < 2.55f)
        {
            light.intensity += 0.05f;
        }
    }

}
