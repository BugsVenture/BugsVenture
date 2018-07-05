using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IBaseEnemy
{

    int Health { get; set; }
    float MaxHearing { get; set; }
    float MaxSight { get; set; }

    bool GotEffect { get; set; }

    void Attack();
    void StopAttack();

    bool MoveTo(Vector3 pos, float threshold = 1.0f);

    void GetDamage(int value);

    void DestroyEnemy();

    void StopMovement();
    void StartMovement();

    void ReceiveSound();

    void NearPlayer();
    void AwayFromPlayer();

    void Rotate(float angle);
    void GetEffect(IEffect effect);

    bool EffectActive();

    void ChangeSpeed(float multiplicator);

    bool SearchPlayer();

    Quadrants Quadrant { get; set; }

}
