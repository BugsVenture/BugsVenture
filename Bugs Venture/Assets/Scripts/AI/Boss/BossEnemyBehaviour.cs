using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Brain))]

public class BossEnemyBehaviour : BaseEnemyBehaviour
{
    public float chargeTime = 5;

    public float maxDistWhileCharing = 5;

    public AudioClip chargeSound;

    private bool isCharging = true;

    private Vector3 currOptPos;

    private Vector3 rePos = Vector3.zero;

    private bool repositioned = false;

    private BossEnemy bEnemy;

    private Brain brain;

    private AudioSource aSource;

    private bool isPlayingSound = false;

    protected override void InitClass()
    {
        aSource = GetComponent<AudioSource>();
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

        if (Sight())
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
            return;
        }
        bEnemy.StartMovement();
        bEnemy.InitAttack();
    }

    IEnumerator AttackDelay()
    {
        if (!isPlayingSound)
        {
            aSource.clip = chargeSound;
            aSource.Play();
            isPlayingSound = true;
        }
        yield return new WaitForSeconds(.9f);
        isPlayingSound = false;
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
        if (Sight())
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
        Debug.Log(State);
        if(!bEnemy.IsActive())
        {
            State = EnemyStates.Idle; 
            return;
        }
        if (Sight())
        {
            bEnemy.LookAt(Player.GetInstance().transform.position);
        }
        if (!BossRoom.GetInstance().IsInDarkness() && isCharging)
        {
            State = EnemyStates.Patrol;
        }
        StateSwitch();
    }

    protected override void Patrol()
    {
        bEnemy.StartMovement();
        if (bEnemy.MoveTo(bEnemy.bossRoom.CalculateClosestEntryPoint(this.transform.position), 2))
        {
            State = EnemyStates.Idle;
        }
    }
}
