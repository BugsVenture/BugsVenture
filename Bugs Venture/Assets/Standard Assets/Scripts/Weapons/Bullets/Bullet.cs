using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IBullet
{
    //Public
    public float bulletSpeed = 10f;
    public int Damage;
    public GameObject HitEffect;


    //Private
    private Rigidbody RigidBody;
    private float hitEffectDuration = 5f;

    void Start()
    {
        RigidBody = GetComponent<Rigidbody>();       
    }

    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            IBaseEnemy enemy = (IBaseEnemy)col.gameObject.GetComponent<IBaseEnemy>();
            enemy.GetDamage(Damage);
        }
        DestroyBullet();
        InstantiateHitEffect();
    }

    public void DestroyBullet()
    {
        Destroy(this.gameObject);
    }

    public void InstantiateHitEffect()
    {
        GameObject hitEffect = Instantiate(HitEffect, this.transform.position,this.transform.rotation);
        Destroy(hitEffect, hitEffectDuration);
    }


}
