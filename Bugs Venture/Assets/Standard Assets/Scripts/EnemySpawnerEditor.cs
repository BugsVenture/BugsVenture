using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class EnemySpawnerEditor : MonoBehaviour {

    public int EnemyCount = 0;
    public GameObject[] Enemy;
    public Vector3[] Offset;
    public int[] SpawnCount;

	// Update is called once per frame
	void Update ()
    {
        if (EnemyCount != Enemy.Length)
        {
            Enemy = new GameObject[EnemyCount];
            Offset = new Vector3[EnemyCount];
            SpawnCount = new int[EnemyCount];
        }
		
	}
}
