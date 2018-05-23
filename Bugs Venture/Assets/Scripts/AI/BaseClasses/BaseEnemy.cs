using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]


public abstract class BaseEnemy : MonoBehaviour, IBaseEnemy

{

    public GameObject CurrRoom;
    private Room currRoom;
    protected NavMeshAgent agent; 
    //Public
    public int health = 2;
    public float maxHearing = 15;
    public float maxSight = 10;

    private bool isNearPlayer = false;

    private Quadrants quadrant = Quadrants.LeftTop;

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
        currRoom = CurrRoom.GetComponent<Room>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.health <= 0)
            DestroyEnemy();        
        if (Vector3.Distance(transform.position, Player.GetInstance().transform.position) < maxHearing && !isNearPlayer)
        {
            NearPlayer();
        }
        else if(Vector3.Distance(transform.position, Player.GetInstance().transform.position) > maxHearing && isNearPlayer)
        {
            AwayFromPlayer();
        }
        if (!currRoom.PosInside(this.transform.position))
        {
            currRoom = RoomContainer.GetInstance().GetInsideRoom(this.transform.position);
        }

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
        if (isNearPlayer)
            AwayFromPlayer();
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

    public void SearchPlayer()
    {
        if (MoveTo(currRoom.GetRandomPosInsideQuadrant(quadrant)))
            quadrant++;
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

    public void ReceiveSound()
    {
        BaseEnemyBehaviour behaviour = GetComponent<BaseEnemyBehaviour>();
        behaviour.HearPlayer();
    }

    public void NearPlayer()
    {
        isNearPlayer = true;
        Player.GetInstance().AddEnemy(this);
    }

    public void AwayFromPlayer()
    {
        isNearPlayer = false;
        Player.GetInstance().RemoveEnemy(this);
    }
}
