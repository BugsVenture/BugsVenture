using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsWeapon : BaseWeapon
{
    //Public
    public GameObject PulsParticle;

 
    public override void Attack()
    {
            Instantiate(PulsParticle, this.transform.position, Quaternion.Euler(new Vector3(90, 0, 0)));
    }
 }
