using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveEffectTrigger : MonoBehaviour
{
    //Public
    public Material dissolveMaterial;
    public float speed, max;
    public GameObject DissolveCubeClone;

    //Private
    private float currentY, startTime;

    void Start()
    {
        currentY = 13.56f;
        max = 10f;
    }

    private void Update()
    {
        if (currentY < max)
        {
            dissolveMaterial.SetFloat("_DissolveY", currentY);
            currentY -= Time.deltaTime * speed;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            TriggerEffect();
        }
    }

    private void TriggerEffect()
    {
        startTime = Time.time;
        currentY = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            Destroy(DissolveCubeClone);
            max = 25;
        }
    }
}
