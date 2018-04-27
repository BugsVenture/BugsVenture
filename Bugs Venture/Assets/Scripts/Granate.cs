using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granate : MonoBehaviour {
    //Public
    public float fireRate = 2f;
    public float damage;
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
        if (Input.GetButtonDown("Fire1"))
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
