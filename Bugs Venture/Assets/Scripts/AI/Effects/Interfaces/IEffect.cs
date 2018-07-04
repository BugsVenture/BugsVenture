using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffect
{
    float Duration { get; set; }

    bool IsActive { get; set; }

    Effects effectType { get; }

    void ActivateEffect(IBaseEnemy enemy);

    void DeactivateEffect(IBaseEnemy enemy);

    void HitPlayer(Player player);

    void DontHitPlayer(Player player);
}

