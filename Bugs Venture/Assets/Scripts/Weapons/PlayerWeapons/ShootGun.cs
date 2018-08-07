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

    //Private
    private CameraShake cameraShake;
    private ParticleSystem MuzzleEffect;
    List<Quaternion> bullets;


    void Awake()
    {
        MuzzleEffect = GetComponent<ParticleSystem>();
        MuzzleEffect.Stop();
        cameraShake = GetComponent<CameraShake>();

        bullets = new List<Quaternion>(bulletCount);
        for(int i = 0; i < bulletCount; i++)
        {
            bullets.Add(Quaternion.Euler(Vector3.zero));
        }
    }
	
	

    public new void Attack()
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
            cameraShake.ShakeCam();
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

    public override void Shoot()
    {
    }
}
