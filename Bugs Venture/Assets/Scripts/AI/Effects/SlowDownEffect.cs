using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownEffect : BaseEffect {

    //private float duration = 3;

    public float slowdownValue = .1f;

    //private bool isActive = false;

    private IBaseEnemy enemy;

    private AudioSource aSource; 

    //public float Duration
    //{
    //    get
    //    {
    //        return duration;
    //    }

    //    set
    //    {
    //        duration = value;
    //    }
    //}

    //public bool IsActive {
    //    get
    //    {
    //        return IsActive;
    //    }
    //    set
    //    {
    //        isActive = value;
    //    }
    //}

    public override Effects effectType
    {
        get
        {
            return Effects.SlowDown;
        }
    }

    private void Start()
    {
        aSource = GetComponent<AudioSource>(); 
    }

    public override void ActivateEffect(IBaseEnemy enemy)
    {
        aSource.Play();
        this.enemy = enemy;
        enemy.ChangeSpeed(slowdownValue);
        StartCoroutine(Deactivation());
    }

    IEnumerator Deactivation()
    {
        yield return new WaitForSeconds(duration);
        enemy.ChangeSpeed(1 / slowdownValue);
    }

    public override void HitPlayer(Player player)
    {
        player.ChangeSpeed(slowdownValue);
        StartCoroutine(PlayerDeactivation());
    }

    IEnumerator PlayerDeactivation()
    {
        yield return new WaitForSeconds(duration);
        Player.GetInstance().ChangeSpeed(1 / slowdownValue);
    }

    public override void DeactivateEffect(IBaseEnemy enemy)
    {
        aSource.Stop();
        StopCoroutine(Deactivation());
        enemy.ChangeSpeed(1 / slowdownValue);
    }

    public override void DontHitPlayer(Player player)
    {
        StopCoroutine(PlayerDeactivation());
        player.ChangeSpeed(1 / slowdownValue);
    }
}
