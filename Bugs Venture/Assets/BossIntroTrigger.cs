using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIntroTrigger : MonoBehaviour {

    public GameObject Enemy;

    public GameObject IntroGenerator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            BossEnemy bEnemy = Enemy.GetComponent<BossEnemy>();
            bEnemy.IntroGenerator = IntroGenerator;
            bEnemy.isInIntro = true;
        }
    }
}
