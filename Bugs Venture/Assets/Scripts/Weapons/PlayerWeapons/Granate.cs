using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granate : BaseWeapon{
    

    public override void Attack()
    {
        if (this.lastFire + (1f / this.fireRate) > Time.time) return;
        Rigidbody rocketInstance;
        rocketInstance = Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation) as Rigidbody;
    }
}
