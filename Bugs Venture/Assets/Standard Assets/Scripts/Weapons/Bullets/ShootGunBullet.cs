using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGunBullet : MonoBehaviour, IBullet
{
    //Public
    public int Damage;
    public GameObject HitEffect;

    //Private
    private float hitEffectDuration = 5f;

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
        }
        DestroyBullet();
        
      
    }

    public void DestroyBullet()
    {
            Destroy(this.gameObject);
    }
    
    public void InstantiateHitEffect()
    {
        GameObject hitEffetc = Instantiate(HitEffect, this.transform.position, this.transform.rotation);
        Destroy(hitEffetc,hitEffectDuration);

    }
    
}
