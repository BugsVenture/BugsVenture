using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEffect : MonoBehaviour, IEffect {

    protected bool isActive = false;

    public float duration = 5;

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

    public bool IsActive {
        get
        {
            return isActive;
        }
        set
        {
            isActive = value;
        }
    }

    public abstract Effects effectType { get; }

    public abstract void ActivateEffect(IBaseEnemy enemy);

    public abstract void DeactivateEffect(IBaseEnemy enemy);

    public abstract void HitPlayer(Player player);

    public abstract void DontHitPlayer(Player player);

    // Use this for initialization
    void Start () {
	}

}
