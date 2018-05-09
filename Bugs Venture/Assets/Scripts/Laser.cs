using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour, IWeapon {


    public int bulletSpeed;
    public int fireRate;
    bool fire;
    public float loadTime = 2;
    bool isLoaded = false;


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
            if (!isLoaded)
                LoadLaser();
            else
                Shoot();
            yield return new WaitForSeconds(fireRate);
        }
    }

    void LoadLaser()
    {
        StartCoroutine(LoadDelay());
        
    }

    void Shoot()
    {
        RaycastHit hit;
        Physics.Raycast(Muzzleoffset.position, transform.right, out hit, Mathf.Infinity);
        {
            Debug.DrawRay(Muzzleoffset.position, transform.right * hit.distance, Color.red);
            if (hit.collider.tag == Player.GetInstance().GetComponent<Collider>().tag)
            {
                Player.GetInstance().GetHit();
            }
        }
    }

    IEnumerator LoadDelay()
    {
        
        yield return new WaitForSeconds(loadTime);
        isLoaded = true;
    }
}
