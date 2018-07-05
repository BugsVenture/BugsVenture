using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultWeapon : BaseWeapon {

    
    public override void Attack()
    {
        fire = true;
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        while (fire)
        {
            Shoot();
            yield return new WaitForSeconds(fireRate);
        }
    }
    
    void Shoot()
    {
        Knockback();
        Rigidbody rocketInstance;
        Transform offset = this.transform.GetChild(0);
        rocketInstance = Instantiate(Bullet, offset.position, offset.rotation) as Rigidbody;

    }
}
