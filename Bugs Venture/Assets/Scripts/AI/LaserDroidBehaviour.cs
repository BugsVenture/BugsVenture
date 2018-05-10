using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IBaseEnemy))]
public class LaserDroidBehaviour : BaseEnemyBehaviour
{


    new LaserDroid enemy;

    public float timeToSearch = 10;
    private Vector3 lastPos;

    // Use this for initialization
    void Start()
    {
        enemy = GetComponent<LaserDroid>();
        layerMask = ~(1<<11);
    }

    // Update is called once per frame
    void Update()
    {
        StateSwitch();
    }
    private new void StateSwitch()
    {
        switch (State)
        {
            case EnemyStates.Idle:
                Idle();
                break;
            case EnemyStates.StartAttack:
                StartAttack();

                break;
            case EnemyStates.OnWayToPlayer:
                OnWayToPlayer();
                break;
            case EnemyStates.IsAttacking:
                IsAttacking();
                break;
            case EnemyStates.IsSearching:
                IsSearching();
                break;
        }
    }
    private new void Idle()
    {
        if (Player.GetInstance())
        {
            if (Vector3.Distance(this.transform.position, Player.GetInstance().transform.position) < activationDistance)
            {
                if (Sight())
                    State = EnemyStates.OnWayToPlayer;
            }
            else
                enemy.MoveTo(startPos);
        }
    }
    private new void StartAttack()
    {
        enemy.Attack();
        State = EnemyStates.IsAttacking;
    }
    private new void OnWayToPlayer()
    {
        if (!Sight())
            lastPos = Player.GetInstance().transform.position;
        //State = EnemyStates.IsSearching;


        enemy.StartMovement();
        enemy.MoveTo(Player.GetInstance().transform.position, 1.0f);

        if (Vector3.Distance(this.transform.position, Player.GetInstance().transform.position) < maxAttackRange)
        {
            State = EnemyStates.StartAttack;
        }
    }
    private new void IsAttacking()
    {
        LaserDroid droid = (LaserDroid)enemy;
        if (droid.RotateAround())
            State = EnemyStates.Idle;

        if (Vector3.Distance(this.transform.position, Player.GetInstance().transform.position) > activationDistance)
            State = EnemyStates.Idle;
    }
    private new void IsSearching()
    {
        StartCoroutine(SearchMode());
        enemy.StartMovement();
        enemy.MoveTo(lastPos);
        if (Sight())
            State = EnemyStates.OnWayToPlayer;
        else
            enemy.SearchPlayer();
    }
    IEnumerator SearchMode()
    {
        yield return new WaitForSeconds(timeToSearch);
        State = EnemyStates.Idle;
    }

    protected override void InitClass()
    {

    }
}

