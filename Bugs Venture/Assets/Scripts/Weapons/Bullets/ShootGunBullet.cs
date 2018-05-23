using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGunBullet : MonoBehaviour
{
    //Public
    public int Damage;
    public GameObject HitEffect;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            IBaseEnemy enemy = (IBaseEnemy)col.gameObject.GetComponent<IBaseEnemy>();
            enemy.GetDamage(Damage);
            Destroy(this.gameObject);
        }
            Destroy(this.gameObject);
            Instantiate(HitEffect, this.transform.position, this.transform.rotation);
        
      
    }
}
