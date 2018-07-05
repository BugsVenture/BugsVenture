using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]

public abstract class BaseEnemy : MonoBehaviour, IBaseEnemy
{

    private Room startRoom;
    private Room currRoom;
    protected NavMeshAgent agent;

    //Public
    public int health = 2;
    public float maxHearing = 15;
    public float maxSight = 10;

    public bool gotEffect = false; 

    private bool isNearPlayer = false;

    private bool slowed = false;

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

    public bool GotEffect
    {
        get
        {
            return gotEffect;
        }

        set
        {
            gotEffect = value;
        }
    }

    public Quadrants Quadrant
    {
        get
        {
            return quadrant;
        }
        set
        {
            quadrant = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        RaycastHit hit;
        Physics.Raycast(this.transform.position, -transform.up, out hit, 10);
        startRoom = hit.collider.GetComponent<Room>();
        agent = GetComponent<NavMeshAgent>();
        currRoom = startRoom;
        quadrant = (Quadrants)Random.Range(0, 3);
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

    public void Knockback(float force)
    {
        GetComponent<Rigidbody>().AddForce(this.transform.forward * -1 * force, ForceMode.Impulse);
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
        if (MoveTo(currRoom.GetRandomPosInsideQuadrant(quadrant)))
        {
            if (quadrant == Quadrants.LeftBottom)
            {
                quadrant = 0;
                return true;
            }
            quadrant++;
            return false;
        }
        return false;
    }

    public void StopAttack()
    {
        IWeapon weapon = this.GetComponentInChildren<IWeapon>();
        weapon.Fire = false;
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

    public void GetEffect(IEffect effect)
    {
        switch(effect.effectType)
        {
            case Effects.SlowDown:
                slowed = true;
                effect.ActivateEffect(this);
                break;
        }
    }

    public void Rotate(float angle)
    {
        transform.Rotate(Vector3.up * Time.deltaTime, angle);
    }

    public bool EffectActive()
    {
        return gotEffect;
    }

    public void ChangeSpeed(float multiplicator)
    {
        if ((agent.speed *= multiplicator) > 4)
        {
            agent.speed = 4;
        }
        IWeapon weapon = GetComponentInChildren<IWeapon>();
        weapon.FireRate /= multiplicator;
    }
}
