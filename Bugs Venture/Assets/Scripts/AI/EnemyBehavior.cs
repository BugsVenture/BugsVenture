using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IBaseEnemy))]
public class EnemyBehavior : MonoBehaviour,  IBehavior {

  
    IBaseEnemy enemy;

    public float sightAngle = 45;
    public float activationDistance = 20;
    public float attackRange = 5;
    public float fireRate = 5f;
 
    EnemyStates State;

    float IBehavior.SightAngle
    {
        get
        {
            return sightAngle;
        }

        set
        {
            sightAngle = value;
        }
    }

    float IBehavior.ActivationDistance { get { return activationDistance; } set { activationDistance = value; } }
    float IBehavior.AttackRange { get { return attackRange; } set { attackRange = value; } }
    float IBehavior.FireRate { get { return fireRate; } set { fireRate = value; } }


    // Use this for initialization
    void Start ()
    {
        enemy = GetComponent<IBaseEnemy>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        switch (State)
        {
            case EnemyStates.Idle:
                if (Player.GetInstance())
                {
                    if (Vector3.Distance(this.transform.position, Player.GetInstance().transform.position) < activationDistance)
                    {
                        Vector3 targetDir = Player.GetInstance().transform.position - this.transform.position;
                        if (Vector3.Angle(targetDir, transform.forward) < sightAngle)
                        {
                            State = EnemyStates.OnWayToPlayer;

                        }
                    }
                }
                break;
            case EnemyStates.StartAttack:
                State = EnemyStates.IsAttacking;
                enemy.Attack();

                break;
            case EnemyStates.OnWayToPlayer:

                enemy.MoveTo(Player.GetInstance().transform.position, 1.0f);
                
                if (Vector3.Distance(this.transform.position, Player.GetInstance().transform.position) < attackRange)
                {
                    State = EnemyStates.StartAttack;
                }
                break;
            case EnemyStates.IsAttacking:

                if (Vector3.Distance(this.transform.position, Player.GetInstance().transform.position) > attackRange)
                    enemy.MoveTo(Player.GetInstance().transform.position, 1.0f);
                if (Vector3.Distance(this.transform.position, Player.GetInstance().transform.position) > activationDistance)
                    State = EnemyStates.Idle;
                break;
        }
    }
}
