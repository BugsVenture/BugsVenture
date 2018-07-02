using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour, IWeapon {


    public int bulletSpeed;
    public float fireRate;
    bool fire;
    public float loadTime = 2;
    bool isLoaded = false;

    private ParticleSystem loadParticles;
    public GameObject hitParticles;

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
        loadParticles = GetComponentInChildren<ParticleSystem>();
        loadParticles.Stop();
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
        SetLineRenderer(GetComponent<Transform>().transform.position, GetComponent<Transform>().transform.position);
    }

    void LoadLaser()
    {
        loadParticles.Play();
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
                SetLineRenderer(GetComponent<Transform>().transform.position, hit.point);
            }
        }
    }

    void SetLineRenderer(Vector3 origin, Vector3 hitpoint)
    {
        
        lr.SetPosition(0, origin);
        lr.SetPosition(1, hitpoint);
    }

    IEnumerator LoadDelay()
    {
        
        yield return new WaitForSeconds(loadTime);
        isLoaded = true;
        loadParticles.Stop();
    }
}
