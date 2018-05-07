using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGun : MonoBehaviour
{
    //Public
    public int bulletCount;
    public float spreadAngle;
    public float bulletFireVel = 1;
    public GameObject bullet;
    public Transform BarrelExit;
    public int Damage = 1;

    //Private
    List<Quaternion> bullets;

    void Awake()
    {
        bullets = new List<Quaternion>(bulletCount);
        for(int i = 0; i < bulletCount; i++)
        {
            bullets.Add(Quaternion.Euler(Vector3.zero));
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    void Fire()
    {
        int i = 0;
        foreach(Quaternion quat in bullets)
        {
            bullets[i] = Random.rotation;
            GameObject b = Instantiate(bullet, BarrelExit.position, BarrelExit.rotation);
            b.transform.rotation = Quaternion.RotateTowards(b.transform.rotation, bullets[i], spreadAngle);
            b.GetComponent<Rigidbody>().AddForce(b.transform.forward * bulletFireVel);
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
