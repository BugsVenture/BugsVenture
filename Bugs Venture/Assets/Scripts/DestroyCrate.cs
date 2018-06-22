using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCrate : MonoBehaviour
{
    //Private
    private int CrateHealth = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(CrateHealth == 0)
        {
            Destroy(this.gameObject);
        }
	}

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Bullet"||col.gameObject.tag == "EnemyBullet")
        {
            CrateHealth--;
        }
    }
}
