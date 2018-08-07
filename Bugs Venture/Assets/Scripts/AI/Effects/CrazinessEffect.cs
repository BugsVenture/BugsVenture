using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazinessEffect : BaseEffect {



    public float radius = 5;

    public float minDegrees = -45;

    public float maxDegrees = 45;

    private Vector3 randomPos;

    private IBaseEnemy enemy;
	
	// Update is called once per frame
	void Update () {
        if (enemy != null)
        {
            enemy.StartMovement();
            if (enemy.MoveTo(this.randomPos, .3f))
            {
                randomPos = enemy.CurrRoom.GetRandomPosInsideRoom();
            }
            enemy.Rotate(Random.Range(minDegrees, maxDegrees));
        }
	}

    public override void ActivateEffect(IBaseEnemy enemy)
    {
        this.isActive = true;
        this.enemy = enemy;
        this.randomPos = enemy.CurrRoom.GetRandomPosInsideRoom();
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
        StopCoroutine(Deactivation());
    }

    public override void HitPlayer(Player player)
    {

    }

    public override void DontHitPlayer(Player player)
    {
    }

    private IEnumerator Deactivation()
    {
        yield return new WaitForSeconds(duration);
        isActive = false;
        enemy.StopMovement();
        enemy.RemoveEffect(this);
        enemy = null;
    }
}
