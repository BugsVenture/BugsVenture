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

    protected EnemyStates State = EnemyStates.Idle;
    protected Vector3 randomPos;
    private Patrol patroling;
    private int patrolSize;
    private int currPatrolPath = 0;
    protected Vector3 soundSource;
    private bool disappearedPlayer = false;
    private bool isChecking = false;
    private bool foundPlayer = false; 

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
        StateSwitch();
        if(!disappearedPlayer)
        {
            if(!Sight() && !isChecking)
            {
                StartCoroutine(CheckTimer());
            }
            if(isChecking)
            {
                if(Sight())
                {
                    foundPlayer = true;
                }
                else
                {
                    foundPlayer = false; 
                }
            }
        }
    }

    private IEnumerator CheckTimer()
    {
        isChecking = true;
        yield return new WaitForSeconds(2);
        if(!foundPlayer)
        {
            disappearedPlayer = true;
        }
        isChecking = false;
    }
    virtual protected void StateSwitch()
    {
        if (enemy.EffectActive())
        {
            State = EnemyStates.GotEffect;
        }
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
                    disappearedPlayer = false; 
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
        if (disappearedPlayer)
        {
            State = EnemyStates.IsSearching;
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
        {
            enemy.StopAttack();
            State = EnemyStates.IsSearching;
        }
    }
    virtual protected void IsSearching()
    {
        if(Sight())
        {
            this.enemy.Quadrant = 0;
            State = EnemyStates.OnWayToPlayer;
        }
        if (this.enemy.SearchPlayer())
        {
            State = EnemyStates.Idle;
        }
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
        if(Player.GetInstance() == null)
        {
            return false; 
        }
        RaycastHit rayHit;
        Vector3 targetDir = Player.GetInstance().transform.position - transform.position;
        Physics.Raycast(transform.position, targetDir, out rayHit, sightDistance, layerMask);
        if (rayHit.collider)
        {
            if (IsInAngle(targetDir))
            {
                if (rayHit.collider.tag == Player.GetInstance().tag)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool IsInAngle(Vector3 dir)
    {
        float product = Vector3.Dot(this.transform.forward, dir);
        float currAngle = Mathf.Acos(product / (this.transform.forward.magnitude * dir.magnitude)) * Mathf.Rad2Deg;
        if (currAngle < sightAngle && currAngle > -sightAngle)
        {
            return true;
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
