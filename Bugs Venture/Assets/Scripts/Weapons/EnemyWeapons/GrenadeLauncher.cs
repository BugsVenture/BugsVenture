using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : BaseWeapon {

    public float power = 80;

    public float angle = 30;

    private float mass; 



    private void Awake()
    {
        mass = Bullet.mass;
    }

    public override void Shoot()
    {
        Rigidbody grenadeInstance;
        Transform offset = this.transform.GetChild(0);
        Knockback();
        CalculateShot();

        grenadeInstance = Instantiate(Bullet, offset.position, offset.rotation) as Rigidbody;
        grenadeInstance.transform.Rotate(grenadeInstance.transform.right * angle);
        grenadeInstance.AddForce(grenadeInstance.transform.forward * power, ForceMode.Impulse);
    }

    void CalculateShot()
    {
        float dist = Vector3.Distance(Player.GetInstance().transform.position, this.transform.position) -.5f;
        float v  = (dist * 9.81f) / (Mathf.Sin(angle * Mathf.Deg2Rad * 2));
        float t = (2 * v * Mathf.Sin(angle * Mathf.Deg2Rad)) / 9.81f;
        float a = v / t;
        power = a * mass;
    }
}
