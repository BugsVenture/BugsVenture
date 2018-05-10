using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultWeapon : MonoBehaviour, IWeapon {

    private Transform Muzzleoffset;
    public Rigidbody bullet;

    public int fireRate;
    public int bulletSpeed;

    private bool fire = false;
    
    int IWeapon.FireRate
    {
        get
        {
            return this.fireRate;
        }

        set
        {
            fireRate = value;
        }
    }

    int IWeapon.BulletSpeed
    {
        get { return this.bulletSpeed; }
        set { this.bulletSpeed = value; }
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


    // Use this for initialization
    void Awake ()
    {
        Muzzleoffset = GetComponentInChildren<Transform>();
	}

    public void Attack()
    {
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        while (fire)
        {
            Shoot();
            yield return new WaitForSeconds(fireRate);
        }
    }

   

    void Shoot()
    {
        Rigidbody rocketInstance;
        Transform offset = this.transform.GetChild(0);
        rocketInstance = Instantiate(bullet, offset.position, offset.rotation) as Rigidbody;

    }
}
