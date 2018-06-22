using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IBaseEnemy))]
public abstract class BaseEnemyBehaviour : MonoBehaviour, IBehavior
{

    protected IBaseEnemy enemy;

    public float sightAngle = 45;
    public float activationDistance = 20;
    public float maxAttackRange = 10;
    public float minAttackRange = 5;

    public float fireRate = 5f;
    public float sightDistance =10;
    protected bool patrol = false;
    
    [Tooltip("This variable has only need on patrolling enemies")]
    public int patrolDelay = 15; //This variable will only have an effect when it's an patroling enemy


    protected Vector3 startPos;
    protected LayerMask layerMask; // only add layermasks by |= 

    protected EnemyStates State;
    protected Vector3 randomPos;
    private Patrol patroling;
    private int patrolSize;
    private int currPatrolPath =0;
    protected Vector3 soundSource;

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
    float IBehavior.AttackRange { get { return maxAttackRange; } set { maxAttackRange = value; } }
    float IBehavior.FireRate { get { return fireRate; } set { fireRate = value; } }

    float IBehavior.SightDistance
    {
        get
        {
            return sightDistance;
        }

        set
        {
            sightDistance = value;
        }
    }


    // Use this for initialization
    void Start()
    {
        InitClass();
        enemy = GetComponent<IBaseEnemy>();
        startPos = this.transform.position;
        layerMask = ~(1<<11);
        if (patrol)
        {
            patroling = GetComponentInChildren<Patrol>();
            patrolSize = patroling.GetPathSize();
        }
    }

    protected abstract void InitClass(); //Helper method to reinit basic variables in inherited classes

    // Update is called once per frame
    void Update()
    {
        Debug.Log(State);
        StateSwitch();
    }
    virtual protected void StateSwitch()
    {
        if (enemy.EffectActive())
            State = EnemyStates.GotEffect;
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
            case EnemyStates.Patrol:
                Patrol();
                break;
            case EnemyStates.ExamineSound:
                ExamineSound();
                break;
            case EnemyStates.GotEffect:
                if (!enemy.EffectActive())
                    State = EnemyStates.Idle;
                break;
        }
    }
    virtual protected void Idle()
    {
        enemy.StopMovement();
        if (Player.GetInstance())
        {
            if (Vector3.Distance(this.transform.position, Player.GetInstance().transform.position) < activationDistance)
            {
                if (Sight() || Vector3.Distance(this.transform.position, Player.GetInstance().transform.position) < 2)
                {
                    State = EnemyStates.OnWayToPlayer;
                }
                else if (patrol)
                    State = EnemyStates.Patrol;
            }
            else
                enemy.MoveTo(startPos, 0.0f);
        }
    }
    virtual protected void StartAttack()
    {
        enemy.Attack(); // Starts the enemy attack Coroutine
        State = EnemyStates.IsAttacking;
    }
    virtual protected void OnWayToPlayer()
    {
        enemy.MoveTo(Player.GetInstance().transform.position, 1.0f);

        if (Vector3.Distance(this.transform.position, Player.GetInstance().transform.position) < minAttackRange)
        {
            State = EnemyStates.StartAttack;
        }
    }
    virtual protected void IsAttacking()
    {
        if (!Sight())
        {
            enemy.StopAttack();
            State = EnemyStates.Idle;
            return;
        }
        DefaultEnemy defaultEnemy = (DefaultEnemy)enemy;
        defaultEnemy.LookAt(Player.GetInstance().transform.position);
        if (Vector3.Distance(this.transform.position, Player.GetInstance().transform.position) > minAttackRange)
            enemy.MoveTo(Player.GetInstance().transform.position, minAttackRange);
        else
        {
            if (Vector3.Equals(randomPos, Vector3.zero))
                CalculateRandomPos();
            if (enemy.MoveTo(randomPos, 0.0f))
                CalculateRandomPos();
        }
        if (Vector3.Distance(this.transform.position, Player.GetInstance().transform.position) > activationDistance)
            State = EnemyStates.Idle;
    }
    virtual protected void IsSearching()
    {

    }
    virtual protected void Patrol()
    {
        if (Sight())
            State = EnemyStates.OnWayToPlayer;
        if (currPatrolPath < patrolSize)
        {
            if (enemy.MoveTo(patroling.NextPath(currPatrolPath)))
            {
                currPatrolPath++;
            }
        }
        else
        {
            if (enemy.MoveTo(startPos))
                StartCoroutine(PatrolDelay(patrolDelay));
        }        
    }
    virtual protected void ExamineSound()
    {
        if (Sight())
            State = EnemyStates.OnWayToPlayer;
        if(enemy.MoveTo(soundSource))
        {
            State = EnemyStates.Idle;
        }
    }
    IEnumerator PatrolDelay(int delay)
    {
        yield return new WaitForSeconds(delay);
        currPatrolPath = 0;
    }
    protected bool Sight()
    {
        RaycastHit rayHit;
        Vector3 targetDir = Player.GetInstance().transform.position - transform.position;
        Physics.Raycast(transform.position, targetDir, out rayHit, sightDistance, layerMask);
        Debug.DrawRay(transform.position, targetDir, Color.cyan);
        if (rayHit.collider)
        {
            if (rayHit.collider.tag == Player.GetInstance().tag)
            {
                return true;

            }
        }
        return false;
    }
    protected void CalculateRandomPos()
    {
        randomPos = new Vector3(Random.Range(transform.position.x - 5, transform.position.x + 5), transform.position.y, Random.Range(transform.position.z - 5, transform.position.z + 5));        
    }

    public void HearPlayer()
    {
        if (State == EnemyStates.Idle || State == EnemyStates.Patrol || State == EnemyStates.IsSearching)
        {
            soundSource = Player.GetInstance().transform.position;
            State = EnemyStates.ExamineSound;
        }
    }
}
