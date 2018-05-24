using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    private EnemySpawnerEditor editor;

	void Start () {
        editor = GetComponentInChildren<EnemySpawnerEditor>();
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("SpawnEnemies"))
        {
            for (int i = 0; i < editor.EnemyCount; i++)
            {
                for (int j = 0; j < editor.SpawnCount[i]; j++)
                {
                    Instantiate<GameObject>(editor.Enemy[i], this.transform.position + editor.Offset[i] + new Vector3(j,0,0), this.transform.rotation);
                }
            }
        }
    }
}
