using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultWeapon : BaseWeapon {

    
    public override void Shoot()
    {
        Knockback();
        Rigidbody rocketInstance;
        Transform offset = this.transform.GetChild(0);
        rocketInstance = Instantiate(Bullet, offset.position, offset.rotation) as Rigidbody;
    }
}
