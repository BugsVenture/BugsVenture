﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]


public abstract class BaseEnemy : MonoBehaviour, IBaseEnemy

{


    protected NavMeshAgent agent; 
    //Public
    public int health = 2;
    public float maxHearing = 15;
    public float maxSight = 10;


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

    public virtual void Attack()
    {
        IWeapon weapon = this.GetComponentInChildren<IWeapon>();
        weapon.Fire = true;
        weapon.Attack();
    }

    public void StopMovement()
    {
        agent.isStopped = true;
    }
    public virtual void StartMovement()
    {
        agent.isStopped = false;
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

    public bool MoveTo(Vector3 pos, float threshold = 1.0f)
    {
        
        StartMovement();
        agent.SetDestination(pos);
        if (Vector3.Distance(this.transform.position, pos) < threshold)
        {
            StopMovement();
            return true;
        }
        return false;
    }

    public bool SearchPlayer()
    {
        return false;
    }

    public void StopAttack()
    {
        IWeapon weapon = this.GetComponentInChildren<IWeapon>();
        weapon.Fire = false;
        weapon.Attack();
    }

    public void LookAt(Vector3 pos)
    {
        transform.LookAt(pos);
    }
}
