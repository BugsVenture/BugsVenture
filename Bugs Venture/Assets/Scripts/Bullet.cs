using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Public
    public float bulletSpeed = 10f;
    public int Damage; 
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
            Destroy(this.gameObject);
        }
        if(col.gameObject.tag == "Wall" || col.gameObject.tag == "Door")
        {
            Destroy(this.gameObject);
        }
    }

    
}
