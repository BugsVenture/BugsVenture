using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour, IBullet {


    private bool hit = false;

    public GameObject effect;

    public float destroyDelay = 1;

    private AudioSource aSource;


    private void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    public void DestroyBullet()
    {
        aSource.Stop();
        Destroy(this.gameObject); //TODO: VFX 
    }

    public void InstantiateHitEffect()
    {
        Instantiate(effect, this.transform.position, this.transform.rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject != null && !hit)
        {
            hit = true;
            StartCoroutine(DetonateDelay());
        }
    }

    void Detonate()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        Destroy(GetComponent<SphereCollider>());
        InstantiateHitEffect();

    }

    IEnumerator DetonateDelay()
    {
        yield return new WaitForSeconds(1.0f);
        Detonate();
    }

    IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(destroyDelay);
        DestroyBullet();
    }
}
