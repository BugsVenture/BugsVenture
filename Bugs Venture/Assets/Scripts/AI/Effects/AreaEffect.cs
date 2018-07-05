using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IEffect))]
public class AreaEffect : MonoBehaviour
{
    //FICK DIE HENNE
    IEffect effect;
    Player player;
    List<IBaseEnemy> enemies = new List<IBaseEnemy>();

    private void Start()
    {
        effect = GetComponent<IEffect>();
        DestroyDelay();
    }

    IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(GetComponent<IEffect>().Duration);
        foreach(IBaseEnemy enemy  in enemies)
        {

        }
        if(player != null)
        {
            player.RemoveEffect(effect);
        }
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        IBaseEnemy enemy = other.GetComponent<IBaseEnemy>();
        enemies.Add(enemy);
        if (player != null)
        {
            player.GetEffect(effect);
        }
        if (enemy != null)
        {
            enemy.GetEffect(this.GetComponent<IEffect>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponent<Player>();
        IBaseEnemy enemy = other.GetComponent<IBaseEnemy>();
        enemies.Remove(enemy);
        if (player != null)
        {
            player.RemoveEffect(GetComponent<IEffect>());
        }
    }
}
