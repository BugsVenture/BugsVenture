﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazinessEffect : BaseEffect {


    public float duration = 5;

    public float radius = 5;

    public float minDegrees = -45;

    public float maxDegrees = 45;

    private bool isActive = false;

    private Vector3 randomPos;

    private IBaseEnemy enemy;

    public float Duration
    {
        get
        {
            return duration;
        }

        set
        {
            duration = value;
        }
    }

    public bool IsActive
    {
        get
        {
            return isActive;
        }

        set
        {
            isActive = value;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (enemy != null)
        {
            enemy.StartMovement();
            if (enemy.MoveTo(this.randomPos, .3f))
            {
                randomPos = RandomPosInRadius();
            }
            enemy.Rotate(Random.Range(minDegrees, maxDegrees));
        }
	}

    public override void ActivateEffect(IBaseEnemy enemy)
    {
        this.isActive = true;
        this.randomPos = RandomPosInRadius();
        this.enemy = enemy;
        this.enemy.GotEffect = true;
        StartCoroutine(Deactivation());
    }

    public override Effects effectType
    {
        get
        {
            return Effects.Craziness;
        }
    }

    public override void DeactivateEffect(IBaseEnemy enemy)
    {
        throw new System.NotImplementedException();
    }

    public override void HitPlayer(Player player)
    {

    }

    public override void DontHitPlayer(Player player)
    {
        throw new System.NotImplementedException();
    }

    private Vector3 RandomPosInRadius()
    {
        return new Vector3(Random.Range(this.transform.position.x - radius, this.transform.position.x + radius), this.transform.position.y, Random.Range(this.transform.position.z - radius, this.transform.position.z + radius));
    }

    private IEnumerator Deactivation()
    {
        yield return new WaitForSeconds(duration);
        isActive = false;
        enemy.StopMovement();
        enemy.GotEffect = false;
        enemy = null;
    }
}
