using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Brain))]

public class BossEnemyBehaviour : BaseEnemyBehaviour
{
    public float chargeTime = 5;

    public float maxDistWhileCharing = 5;

    private bool isCharging = true;

    private Vector3 currOptPos;

    private Vector3 rePos = Vector3.zero;

    private bool repositioned = false; 

    private BossEnemy bEnemy;

    private Brain brain;
    
    protected override void InitClass()
    {
        StartCoroutine(Charge());
        bEnemy = GetComponent<BossEnemy>();
        brain = GetComponent<Brain>();
    }

    IEnumerator Charge()
    {
        isCharging = true;
        yield return new WaitForSeconds(chargeTime);
        isCharging = false; 
    }

    protected override void Idle()
    {
        if (!bEnemy.IsActive())
            return;

        if(Sight())
        {
            State = EnemyStates.StartAttack;
            return;
        }
         State = EnemyStates.IsSearching; 
    }

    protected override void StartAttack()
    {
        bEnemy.StopMovement();
        if (!Sight())
        {
            State = EnemyStates.Idle;
            return;
        }
        if (isCharging)
        {
            Reposition();
            return;
        }
        if (bEnemy.isAttacking)
        {
            StartCoroutine(AttackDelay());
            StopCoroutine(AttackDelay());
            return;
        }
        bEnemy.StartMovement();
        bEnemy.InitAttack();
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(.5f);
        State = EnemyStates.IsAttacking;
    }

    private void Reposition()
    {
        if (!repositioned)
        {
            rePos = bEnemy.CurrRoom.GetRandomPosInsideRoom();
            repositioned = true;
        }
        bEnemy.StartMovement();
        if (Vector3.Distance(this.transform.position, rePos) >= maxDistWhileCharing)
        {
            if (bEnemy.MoveTo(rePos))
                repositioned = false;
        }
        else
        {
            repositioned = false;
        }
    }

    protected override void IsAttacking()
    {
        if (bEnemy.isAttacking)
        {
            bEnemy.Attack();
        }
        else
        {
            StartCoroutine(Charge());
            State = EnemyStates.Idle;
        }
    }

    protected override void IsSearching()
    {
        bEnemy.StartMovement();
        if(Sight())
        {
            brain.DeactivateBrain();
            State = EnemyStates.StartAttack;
        }
        if (!brain.isActive)
        {
            brain.ActivateBrain();
        }
        bEnemy.ExamineHint(brain.hint);
    }

    void Update()
    {
        if(Sight())
        {
            bEnemy.LookAt(Player.GetInstance().transform.position);
        }
        if(!BossRoom.GetInstance().IsInDarkness() && isCharging)
        {
            State = EnemyStates.Patrol; 
        }
        Debug.Log(State);
        StateSwitch();
    }

    protected override void Patrol()
    {
        bEnemy.StartMovement();
        if (bEnemy.MoveTo(bEnemy.bossRoom.CalculateClosestEntryPoint(this.transform.position),2))
        {
            State = EnemyStates.Idle;
        }
    }
}
