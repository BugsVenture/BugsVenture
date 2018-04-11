using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IBaseEnemy
{

    int Health { get; set; }
    float MaxHearing { get; set; }
    float MaxSight { get; set; }

    IEnumerator Attack();

    void MoveToPlayer();

    void GetDamage(int value);

    void DestroyEnemy();


}
