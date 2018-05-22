using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour, IWeapon {


    public int bulletSpeed;
    public float fireRate;
    bool fire;
    public float loadTime = 2;
    bool isLoaded = false;

    private LineRenderer lr;
    Transform Muzzleoffset;

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
        lr = GetComponentInChildren<LineRenderer>();
    }

    public void Attack()
    {
        StartCoroutine(AttackRoutine());
    }
    IEnumerator AttackRoutine()
    {
        isLoaded = false;
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
            if(hit.collider)
            {
                lr.SetPosition(1, hit.transform.TransformPoint(hit.point));
                Debug.Log(hit.point);
            }
        }
    }

    IEnumerator LoadDelay()
    {
        
        yield return new WaitForSeconds(loadTime);
        isLoaded = true;
    }
}
