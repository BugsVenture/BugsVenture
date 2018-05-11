using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour, IWeapon {

    public float fireRate = 2f;
    public int bulletSpeed = 20;
    public int damage;
    public Transform BulletSpawn;
    public Rigidbody Bullet;

    protected bool fire;


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
            fireRate = value;
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

    public abstract void Attack();
}
