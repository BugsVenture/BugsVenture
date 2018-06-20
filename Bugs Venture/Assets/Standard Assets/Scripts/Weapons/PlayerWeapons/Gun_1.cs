using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_1 : BaseWeapon
{
    
    //Private
    private ParticleSystem MuzzleEffect;
    private CameraShake cameraShake;

    void Start()
    {
        MuzzleEffect = GetComponent<ParticleSystem>();
        MuzzleEffect.Stop();
        cameraShake = GameObject.FindObjectOfType<CameraShake>();
    }

    public override void Attack()
    {
        if (this.lastFire + (1f / this.fireRate) > Time.time) return;
        Rigidbody rocketInstance;
        rocketInstance = Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation) as Rigidbody;
        MuzzleEffect.Stop();
        MuzzleEffect.Play();
        cameraShake.ShakeCam();
    }
}

