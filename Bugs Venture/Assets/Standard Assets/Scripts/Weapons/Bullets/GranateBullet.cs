using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranateBullet : MonoBehaviour
{

    //Public
    public float bulletSpeed = 10f;
    public GameObject GranateEffect;

    //Private
    private CameraShake cameraShake;
    private Rigidbody RigidBody;
    private float granateEffectDuration = 5f;

    void Start()
    {
        cameraShake = GetComponent<CameraShake>();
        RigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
        Destroy(this.gameObject, 2);
    }
    void OnCollisionEnter(Collision col)
    {
        GameObject granateEffect = Instantiate(GranateEffect, this.transform.position, Quaternion.Euler(new Vector3(-90, 90, 0)));
        cameraShake.ShakeCam();
        Destroy(this.gameObject);
        Destroy(granateEffect, granateEffectDuration);
    }
}
