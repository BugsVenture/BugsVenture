using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyBehavior : MonoBehaviour {

    Enemy enemy; 
	// Use this for initialization
	void Start ()
    {
        enemy = GetComponent<Enemy>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		//if(Vector3.Distance(enemy.transform.position, ))
	}
}
