using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class LaserDroid : DefaultEnemy {



    public bool RotateRight;
    public float RotationSpeed;
    public float maxRotation = 360;

    private float currAngle;
    private float currRotation = 0.0f;


    public new void Attack()
    {
        IWeapon[] weapons = GetComponentsInChildren<IWeapon>();
        StopMovement();
        currAngle = this.transform.eulerAngles.y;
        foreach (IWeapon weapon in weapons)
        {
            weapon.Fire = true;
            StartCoroutine(weapon.Attack());
        }

    }


    public bool RotateAround()
    {
        if(RotateRight)
        {
            if (currRotation < maxRotation)
            {
                this.transform.Rotate(Vector3.up * Time.deltaTime * RotationSpeed);
                if (transform.eulerAngles.y > currAngle)
                    currRotation += transform.eulerAngles.y - currAngle;
                else
                {
                    currAngle -= 360;
                    currRotation += transform.eulerAngles.y - currAngle;
                }
                currAngle = transform.eulerAngles.y;

            }
            else
            {
                currRotation = 0;
                return true;
            }
        }
        return false;
    }

    public new void  StopAttack()
    {
        IWeapon[] weapons = GetComponentsInChildren<IWeapon>();
        foreach (IWeapon weapon in weapons)
            weapon.Fire = false;
    }


}
