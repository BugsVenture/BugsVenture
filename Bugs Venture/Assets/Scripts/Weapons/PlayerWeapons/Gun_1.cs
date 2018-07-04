﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_1 : BaseWeapon
{
    //Public
    public ParticleSystem MuzzleEffect;
    
    //Private
    public CameraShake cameraShake;


    void Start()
    {
        cameraShake = FindObjectOfType<CameraShake>();
        MuzzleEffect.Stop();
    }

    public override void Attack()
    {
        if (this.lastFire + (1f - this.fireRate) < Time.time)
        {
            Rigidbody rocketInstance;
            rocketInstance = Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation) as Rigidbody;
            MuzzleEffect.Stop();
            MuzzleEffect.Play();
            //cameraShake.CamKnockBack();
            
        }
    }

}

