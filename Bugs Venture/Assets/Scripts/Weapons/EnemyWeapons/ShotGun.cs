using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : BaseWeapon {

    public int bulletCount = 5;
    public int spreadAngle = 45;

    public override void Attack()
    {
        fire = true;
        StartCoroutine(AttackRoutine());
    }
    
    

    IEnumerator AttackRoutine()
    {
        while(fire)
        {
            Shoot();
            yield return new WaitForSeconds(fireRate);
        }
    }

    private void Shoot()
    {
        Rigidbody rocketInstance;
        Transform offset = this.transform.GetChild(0);
        
        for (int i = 0; i < bulletCount; i++)
        {
            Vector3 currAngle = offset.rotation.eulerAngles;
            currAngle.y += Random.Range(-spreadAngle / 2, spreadAngle / 2);
            offset.rotation.eulerAngles.Set(currAngle.x, currAngle.y, currAngle.z);
            rocketInstance = Instantiate(Bullet, offset.position, offset.rotation) as Rigidbody;
        }

    }
}
