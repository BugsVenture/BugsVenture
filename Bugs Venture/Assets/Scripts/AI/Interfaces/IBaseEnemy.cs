﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IBaseEnemy
{

    int Health { get; set; }
    float MaxHearing { get; set; }
    float MaxSight { get; set; }

    IEnumerator Attack();

    bool MoveTo(Vector3 pos, float threshold);

    void GetDamage(int value);

    void DestroyEnemy();


}
