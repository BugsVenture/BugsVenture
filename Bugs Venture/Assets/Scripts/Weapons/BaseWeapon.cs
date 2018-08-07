﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour, IWeapon {

    //Delay between shots.
    public float fireRate = 5;
    public int bulletSpeed = 20;
    public int damagePerBullet;
    protected Transform BulletSpawn;
    public Rigidbody Bullet;

    public float knockbackForce = 5;

    protected bool fire;

    private bool isAttacking = false; 

    private float maxFireRate; 

    void Awake()
    {
        BulletSpawn = GetComponentInChildren<Transform>();
    }

    private void Start()
    {
        maxFireRate = fireRate;
    }
    public void SetFireRate(float fRate)
    {
        if (fRate < maxFireRate)
        {
            fireRate = maxFireRate;
            return;
        }
        fireRate = fRate;
    }

    //Private
    protected float lastFire = float.MinValue;

    float IWeapon.FireRate
    {
        get
        {
            return fireRate;
        }

        set
        {
            if(value < maxFireRate)
            {
                fireRate = maxFireRate;
                return;
            }
            fireRate = value;
            Debug.Log(fireRate);
        }
    }

    int IWeapon.BulletSpeed
    {
        get
        {
            return bulletSpeed;
        }

        set
        {
            bulletSpeed = value;
        }
    }
    bool IWeapon.Fire
    {
        get
        {
            return fire;
        }
        set
        {
            fire = value;
        }
    }

    public virtual void Attack()
    {
        fire = true;
        if (!isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    public IEnumerator AttackRoutine()
    {
        isAttacking = true;
        while (fire)
        {
            Shoot();
            yield return new WaitForSeconds(fireRate);
        }
    }

    public void StopAttack()
    {
        fire = false;
        StopCoroutine(AttackRoutine());
        isAttacking = false;
    }

    public abstract void Shoot();

    public void Knockback()
    {
        this.gameObject.GetComponentInParent<Rigidbody>().AddForce(GetComponentInParent<Transform>().right * -1 * knockbackForce, ForceMode.Impulse);
    }
}
