using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour, IBullet
{
    //Public
    public float bulletSpeed = 10f;
    public int Damage;
    public GameObject HitEffect;
    public Effects effectType;
    private IEffect effect;

    //Private
    private Rigidbody RigidBody;

    void Start()
    {
        RigidBody = GetComponent<Rigidbody>();
        if(effectType != Effects.None)
        {
            effect = GetComponent<IEffect>();
        }
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
            if (effectType != Effects.None)
                enemy.GetEffect(effect);
            enemy.GetDamage(Damage);
        }
        InstantiateHitEffect();
        DestroyBullet();
    }

    public void DestroyBullet()
    {
        Destroy(this.gameObject);
    }

    public void InstantiateHitEffect()
    {
        GameObject hitEffect = Instantiate(HitEffect, this.transform.position,this.transform.rotation);
        Destroy(hitEffect, effect.Duration);
    }

    public void Shoot()
    {
        throw new System.NotImplementedException();
    }
}
