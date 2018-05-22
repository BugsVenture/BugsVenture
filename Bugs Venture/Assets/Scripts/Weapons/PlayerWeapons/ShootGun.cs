using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGun : BaseWeapon
{
    //Public
    public int bulletCount;
    public float spreadAngle;
    public float bulletFireVel = 1;
    public GameObject bullet;
    public Transform BarrelExit;
    public int Damage = 1;
    public CameraShake cameraShake;

    //Private
    private ParticleSystem MuzzleEffect;
    List<Quaternion> bullets;


    void Awake()
    {
        MuzzleEffect = GetComponent<ParticleSystem>();
        MuzzleEffect.Stop();
        cameraShake = GameObject.FindObjectOfType<CameraShake>();

        bullets = new List<Quaternion>(bulletCount);
        for(int i = 0; i < bulletCount; i++)
        {
            bullets.Add(Quaternion.Euler(Vector3.zero));
        }
    }
	
	

    public override void Attack()
    {
        int i = 0;
        foreach(Quaternion quat in bullets)
        {
            MuzzleEffect.Stop();
            MuzzleEffect.Play();
            bullets[i] = Random.rotation;
            GameObject b = Instantiate(bullet, BarrelExit.position, BarrelExit.rotation);
            b.transform.rotation = Quaternion.RotateTowards(b.transform.rotation, bullets[i], spreadAngle);
            b.GetComponent<Rigidbody>().AddForce(b.transform.forward * bulletFireVel);
            cameraShake.shouldShake = true;
            i++;
            Destroy(b.gameObject,2);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            IBaseEnemy enemy = (IBaseEnemy)col.gameObject.GetComponent<IBaseEnemy>();
            enemy.GetDamage(Damage);
        }
    }

}
