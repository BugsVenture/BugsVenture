using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Public
    public float bulletSpeed = 10f;
    public int Damage;
    public GameObject HitEffect;

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
        if(col.gameObject.tag == "Enemy")
        {
            IBaseEnemy enemy = (IBaseEnemy)col.gameObject.GetComponent<IBaseEnemy>();
            enemy.GetDamage(Damage);
        }
        Destroy(this.gameObject);
        Instantiate(HitEffect, this.transform.position,this.transform.rotation); 
    }

    
}
