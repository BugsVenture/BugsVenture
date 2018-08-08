using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class BossEnemy : BaseEnemy
{
    public float dashSpeed = 20;

    public float dashEndOffset = 5;

    private Vector3 dashPoint;

    public BossRoom bossRoom;

    public AudioClip dashSound;

    private bool isActive = false;

    private int health = 3;

    public float damageDelay = 2;

    private bool soundPlaying = false;

    public GameObject FirstGenerator; 

    [HideInInspector]
    public bool isAttacking = false;
    [HideInInspector]
    public GameObject IntroGenerator;
    [HideInInspector]
    public bool isInIntro = false; 

    public override void Attack()
    {
        if(Dash())
        {
            isAttacking = false; 
        }
    }

    public void ExamineHint(Vector3 hintpos)
    {
        StartMovement();
        hintpos.y = this.transform.position.y;
        MoveTo(bossRoom.CalculateClosestEntryPoint(hintpos), .1f);
        this.transform.LookAt(hintpos);
    }

    public void GetDamage()
    {
        health--;
        StartCoroutine(DamageDelay());
        if(health == 0)
        {
            DestroyEnemy();
        }
    }

    IEnumerator DamageDelay()
    {
        isActive = false;
        yield return new WaitForSeconds(this.damageDelay);
        isActive = true;
    }

    public new void DestroyEnemy()
    {
        Destroy(this);
    }

    private void CalculateDashPoint()
    {
        RaycastHit hit;
       
        if(Physics.Raycast(Player.GetInstance().transform.position, this.transform.forward, out hit, dashEndOffset))
        {
            dashPoint = hit.point - transform.forward;
            return;
        }

        dashPoint = Player.GetInstance().transform.position + (this.transform.forward * dashEndOffset);
    }

    public void InitAttack()
    {
        isAttacking = true;
        CalculateDashPoint();
    }

    public bool IsActive()
    {
        return isActive;
    }

    private void Start()
    {
        aSource = GetComponent<AudioSource>();
        bossRoom = BossRoom.GetInstance();
        RaycastHit hit;
        Physics.Raycast(this.transform.position, -transform.up, out hit, 10);
        startRoom = hit.collider.GetComponent<Room>();
        agent = GetComponent<NavMeshAgent>();
        currRoom = startRoom;
    }

    private void Update()
    {
        if(isAttacking)
        {
            this.transform.LookAt(dashPoint);
        }
        if(isInIntro)
        {
            PlayIntro();
            BossGenerator gen = FirstGenerator.GetComponent<BossGenerator>();
            if(gen.SetIntroCam())
            {

            }
        }
        //if (currRoom.PosInside(Player.GetInstance().transform.position))
        //{
        //    if (isInIntro)
        //    {
        //        isActive = true;
        //    }
        //}
        if(currRoom == null)
        {
            currRoom = bossRoom.GetRoom();

        }
        if (this.health <= 0)
            DestroyEnemy();
        if (Vector3.Distance(transform.position, Player.GetInstance().transform.position) < maxHearing && !isNearPlayer)
        {
            NearPlayer();
        }
        else if (Vector3.Distance(transform.position, Player.GetInstance().transform.position) > maxHearing && isNearPlayer)
        {
            AwayFromPlayer();
        }

    }
    private void PlayIntro()
    {
        dashPoint = IntroGenerator.transform.position;
        if(Dash())
        {
            StartCoroutine(PostIntroScene());
        }
    }

    IEnumerator PostIntroScene()
    {
        yield return new WaitForSeconds(3);
        isInIntro = false;
        isActive = true;
    }

    private bool Dash()
    {
        if(!soundPlaying)
        {
            aSource.clip = dashSound;
            aSource.Play();
        }
        soundPlaying = true;
        this.transform.position = Vector3.Lerp(this.transform.position, dashPoint, dashSpeed* Time.deltaTime);

        if(Vector3.Distance(this.transform.position, dashPoint) < .3f)
        {
            soundPlaying = false; 
            return true;
        }
        return false;
    }
}
