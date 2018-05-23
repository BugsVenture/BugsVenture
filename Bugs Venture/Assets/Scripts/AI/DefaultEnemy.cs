using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]


public class DefaultEnemy : MonoBehaviour, IBaseEnemy

{


    private NavMeshAgent agent; 
    //Public
    public int health;
    public float maxHearing = 15;
    public float maxSight = 10;
    
    public Rigidbody bullet; 


    int IBaseEnemy.Health
    {
        get
        {
            return this.health;
        }

        set
        {
            health = value;
        }
    }

    float IBaseEnemy.MaxHearing
    {
        get
        {
            return this.maxHearing;
        }

        set
        {
            this.maxHearing = value;
        }
    }

    float IBaseEnemy.MaxSight
    {
        get
        {
            return this.maxSight;
        }
        set
        {
            this.maxSight = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.health <= 0)
            DestroyEnemy();
       
    }

    public IEnumerator Attack()
    {
        Rigidbody rocketInstance;
        Transform offset = this.transform.GetChild(0);
        rocketInstance = Instantiate(bullet, offset.position, offset.rotation) as Rigidbody;
        yield return new WaitForSeconds(1);
    }

    public void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }

    void IBaseEnemy.GetDamage(int value)
    {
        this.health -= value;
        Debug.Log(this.health);
    }

    public bool MoveTo(Vector3 pos, float threshold)
    {
        agent.SetDestination(pos);
        if (Vector3.Distance(this.transform.position, pos) < threshold)
            return true;
        return false;
    }
}
