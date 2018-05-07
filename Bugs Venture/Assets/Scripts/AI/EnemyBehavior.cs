using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IBaseEnemy))]
public class EnemyBehavior : MonoBehaviour {

    IBaseEnemy enemy;

    public float SigthAngle = 45;
    public float ActivationDistance = 20;
    public float AttackRange = 5;
    public float fireRate = 5f;

    // Use this for initialization
    void Start ()
    {
        enemy = GetComponent<IBaseEnemy>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Player.GetInstance())
        {
            if (Vector3.Distance(this.transform.position, Player.GetInstance().transform.position) < ActivationDistance)
            {
                Vector3 targetDir = Player.GetInstance().transform.position - this.transform.position;
                if (Vector3.Angle(targetDir, transform.forward) < SigthAngle)
                {
                    if (Vector3.Distance(this.transform.position, Player.GetInstance().transform.position) < AttackRange)
                    {
                        StartCoroutine(enemy.Attack());
                    }
                    else
                    {
                        enemy.MoveTo(Player.GetInstance().transform.position, 1.0f);
                    }
                }
            }
        }
    }
}
