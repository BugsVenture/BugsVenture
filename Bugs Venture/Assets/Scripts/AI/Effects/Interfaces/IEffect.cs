using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffect
{
    float Duration { get; set; }

    bool IsActive { get; set; }

    void ActivateEffect(IBaseEnemy enemy);
}

