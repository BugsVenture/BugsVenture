using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class Player : MonoBehaviour {

    private static Player playerInstance;

    private List<BaseEnemy> enemies = new List<BaseEnemy>();

    public List<IEffect> activeEffects = new List<IEffect>();

    private bool slowed = false;

    private float maxSpeed;

    public static Player GetInstance()
    {
        return playerInstance;
    }

    private void Awake()
    {
        if(playerInstance == null)
            playerInstance = this;

        maxSpeed = GetComponent<CharacterMovement>().moveSpeed;
    }

    public void ChangeSpeed(float multiplicator)
    {
        CharacterMovement movement = GetComponent<CharacterMovement>();
        if((movement.moveSpeed *= multiplicator)>maxSpeed)
        {
            movement.moveSpeed = maxSpeed;
        }
        GunController controller = GetComponentInChildren<GunController>();

        controller.GetWeapon().SetFireRate(controller.GetWeapon().fireRate /= multiplicator);
    }

    public void GetEffect(IEffect effect)
    {
        switch(effect.effectType)
        {
            case Effects.SlowDown:
                if(slowed)
                {
                    return;
                }
                slowed = true;
                activeEffects.Add(effect);
                effect.HitPlayer(this);
                break;
            default:                
                break;
        }
    }

    public void RemoveEffect(IEffect effect)
    {
        switch(effect.effectType)
        {
            case Effects.SlowDown: 
                if(!slowed)
                {
                    return;
                }
                slowed = false; 
                effect.DontHitPlayer(this);
                activeEffects.Remove(effect);
                break;
            default:
                activeEffects.Remove(effect);
                break;
        }
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
        Debug.Log(GetComponent<CharacterMovement>().moveSpeed);
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
