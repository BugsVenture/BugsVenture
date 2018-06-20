using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class Player : MonoBehaviour {

    private static Player playerInstance;

    private List<BaseEnemy> enemies = new List<BaseEnemy>();

    public static Player GetInstance()
    {
        return playerInstance;
    }

    private void Awake()
    {
        if(playerInstance == null)
            playerInstance = this;
    }



    // Use this for initialization
    void Start ()
    {
		
	}

    public void GetHit()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            GetHit();
        }
    }

    // Update is called once per frame
    void Update ()
    {
        //Fire
        if(Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    private void Attack()
    {
        GunController controller = GetComponentInChildren<GunController>();
        controller.GetWeapon().Attack();

        foreach (BaseEnemy enemy in enemies)
            enemy.ReceiveSound();
    }
    public void AddEnemy(BaseEnemy enemy)
    {
        enemies.Add(enemy);
    }
    public void RemoveEnemy(BaseEnemy enemy)
    {
        enemies.Remove(enemy);
    }
}
