using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemy : BaseEnemy
{

	public void LookAt(Vector3 pos)
    {
        transform.LookAt(pos);
    }
}
