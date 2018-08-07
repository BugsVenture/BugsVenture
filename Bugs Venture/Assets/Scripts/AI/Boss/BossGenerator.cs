using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGenerator : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Boss")
        {
            BossEnemy bEnemy = collision.gameObject.GetComponent<BossEnemy>();
            bEnemy.GetDamage();
        }
    }

    void Destroy()
    {
        Destroy(this);
    }
}
