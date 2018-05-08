using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour, IWeapon {


    public int bulletSpeed;
    public int fireRate;
    bool fire;

    Transform Muzzleoffset;

    int IWeapon.FireRate
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

    int IWeapon.BulletSpeed {
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

    void Awake()
    {
        Muzzleoffset = GetComponentInChildren<Transform>();
    }
    IEnumerator IWeapon.Attack()
    {
        while (fire)
        {
            LoadLaser();
        yield return new WaitForSeconds(fireRate);
        }
    }

    void LoadLaser()
    {
        RaycastHit hit;
        Physics.Raycast(Muzzleoffset.position, transform.right, out hit, Mathf.Infinity);
        {
            Debug.DrawRay(Muzzleoffset.position, transform.right * hit.distance, Color.red);
        }
    }

    void Shoot()
    {

    }
}
