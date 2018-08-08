using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class LaserDroid : BaseEnemy {



    public bool RotateRight;
    public float RotationSpeed;
    public float maxRotation = 360;

    private float currAngle;
    private float currRotation = 0.0f;
    private Vector3 atkStartPos = Vector3.zero; 


    public new void Attack()
    {
        IWeapon[] weapons = GetComponentsInChildren<IWeapon>();
        StopMovement();
        atkStartPos = this.transform.position;
        currAngle = this.transform.eulerAngles.y;
        foreach (IWeapon weapon in weapons)
        {
            weapon.Fire = true;
            weapon.Attack();
        }
    }

    
    public bool RotateAround()
    {
        this.transform.position = atkStartPos; 
        if(RotateRight)
        {
            if (currRotation < maxRotation)
            {
                this.transform.Rotate(Vector3.up * Time.deltaTime * RotationSpeed);
                if (transform.eulerAngles.y < currAngle)
                    currAngle -= 360;
                    
                currRotation += transform.eulerAngles.y - currAngle;                
                currAngle = transform.eulerAngles.y;

            }
            else
            {
                currRotation = 0;
                StopAttack();
                return true;
            }
        }
        else
        {
            if (currRotation < maxRotation)
            {
                this.transform.Rotate(-Vector3.up * Time.deltaTime * RotationSpeed);
                if (transform.eulerAngles.y > currAngle)
                    currAngle += 360;
                currRotation += currAngle - transform.eulerAngles.y;
                currAngle = transform.eulerAngles.y;
            }
            else
            {
                currRotation = 0;
                StopAttack();
                return true;
            }
        }
        return false;
    }
    public override void StartMovement()
    {
        StopAttack();
        agent.isStopped = false; 
    }

    public new void  StopAttack()
    {
        IWeapon[] weapons = GetComponentsInChildren<IWeapon>();
        foreach (IWeapon weapon in weapons)
            weapon.Fire = false;
        
    }


}
