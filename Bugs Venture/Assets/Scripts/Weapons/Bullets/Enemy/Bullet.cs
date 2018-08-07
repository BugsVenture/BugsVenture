﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour, IBullet
{
    //Public
    public float bulletSpeed = 10f;
    public int Damage;
    public GameObject HitEffect;
    public Effects effectType;
    public List<AudioClip> audios = new List<AudioClip>();
    private IEffect effect;
    private AudioSource aSource; 
    //Private
    private Rigidbody RigidBody;

    void Start()
    {
        aSource = GetComponent<AudioSource>();
        aSource.clip = audios[Random.Range(0, audios.Count)];
        RigidBody = GetComponent<Rigidbody>();
        aSource.Play();
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
            enemy.GetDamage(Damage);
            if (effectType != Effects.None)
                enemy.GetEffect(effect);
        }
        InstantiateHitEffect();
    }

    public void DestroyBullet()
    {
        Destroy(this.gameObject);
    }

    public void InstantiateHitEffect()
    {
        GameObject hitEffect = Instantiate(HitEffect, this.transform.position,this.transform.rotation);
        DestroyBullet();
        Destroy(hitEffect, effect.Duration);
    }
}
