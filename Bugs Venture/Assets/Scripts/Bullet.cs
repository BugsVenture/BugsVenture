using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Public
    public float bulletSpeed = 10f;

    //Private
    private Rigidbody RigidBody;

    void Start()
    {
        RigidBody = GetComponent<Rigidbody>();       
    }

    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
        Destroy(this.gameObject, 2);
    }
    void OnCollisionEnter(Collision col)
    {
        Destroy(this.gameObject);
    }

    
}
