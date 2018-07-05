using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMP : MonoBehaviour {



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
        foreach (IBaseEnemy enemy in enemies)
        {
            enemy.RemoveEffect(effect);
        }
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        IBaseEnemy enemy = other.GetComponent<IBaseEnemy>();
        enemies.Add(enemy);

        if (enemy != null)
        {
            enemy.GetEffect(this.GetComponent<IEffect>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        IBaseEnemy enemy = other.GetComponent<IBaseEnemy>();
        enemies.Remove(enemy);
    }
}
