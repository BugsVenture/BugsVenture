using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunEnemyBehaviour : BaseEnemyBehaviour {

	protected override void InitClass()
    {
        patrol = true;
    }

    protected override void IsAttacking()
    {
        ShotGunEnemy shotEnemy = (ShotGunEnemy)enemy;
        shotEnemy.LookAt(Player.GetInstance().transform.position);
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
}
