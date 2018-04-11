using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]


public class Enemy : MonoBehaviour, IBaseEnemy

{
    private NavMeshAgent agent; 
    //Public
    public int health;
    public float maxHearing = 15;
    public float maxSight = 10;
    public float SigthAngle = 45;
    public float ActivationDistance = 20;
    public float AttackRange = 5;


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
        if (Vector3.Distance(this.transform.position, Player.GetInstance().transform.position) < ActivationDistance)
        {
            Vector3 targetDir = Player.GetInstance().transform.position - this.transform.position;
            if (Vector3.Angle(targetDir, transform.forward) < SigthAngle)
            {
                if(Vector3.Distance(this.transform.position, Player.GetInstance().transform.position)< AttackRange)
                {
                    Attack();
                }
                else
                {
                    MoveToPlayer();
                }
            }
        }

    }

    public void Attack()
    {

    }

    public void MoveToPlayer()
    {
        this.agent.SetDestination(Player.GetInstance().transform.position);
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
}
