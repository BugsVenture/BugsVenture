using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class LaserDroid : MonoBehaviour, IBaseEnemy {

    private NavMeshAgent agent;

    public int health;
    public float maxHearing;
    public float maxSight;


    int IBaseEnemy.Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }

    float IBaseEnemy.MaxHearing {
        get
        {
            return maxHearing;
        }

        set
        {
            maxHearing = value;
        }
    }
    float IBaseEnemy.MaxSight {
        get
        {
            return maxSight; 
        }

        set
        {
            maxSight = value;
        }
    }

    public void Attack()
    {
        IWeapon[] weapons = GetComponents<IWeapon>();
        Debug.Log(weapons.Length);
        foreach (IWeapon weapon in weapons)
        {
            weapon.Fire = true;
            StartCoroutine(weapon.Attack());
        }

    }

    public void DestroyEnemy()
    {

    }

    public void GetDamage(int value)
    {
        health -= value;
    }

    public bool MoveTo(Vector3 pos, float threshold)
    {
        agent.SetDestination(pos);
        if (Vector3.Distance(this.transform.position, pos) < threshold)
            return true;
        return false;
    }



    public void StopAttack()
    {
        IWeapon[] weapons = GetComponents<IWeapon>();
        foreach (IWeapon weapon in weapons)
            weapon.Fire = false;
    }


}
