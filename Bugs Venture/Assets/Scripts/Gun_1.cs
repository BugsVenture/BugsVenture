using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_1 : MonoBehaviour {
    //Public
    public float fireRate = 2f;
    public int damage;
    public Transform BulletSpawn;
    public Rigidbody Bullet;

    //Private
    private float lastFire = float.MinValue;



    // Use this for initialization
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        //Fire
        if (Input.GetButton("Fire1"))
        {
            Fire();
        }
        if (Input.GetAxisRaw("FireAxis") > 0f)
        {
            Fire();
        }
    }

    public void Fire()
    {
        if (this.lastFire + (1f / this.fireRate) > Time.time) return;
        Rigidbody rocketInstance;
        rocketInstance = Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation) as Rigidbody;

    }
}
