using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{

    public Vector3 hint;

    public float hintDelay = 3;

    public bool isActive = false;

    public float range = 1;

    public void ActivateBrain()
    {
        hint = CalculateRandomPosAroundPlayer();
        StartCoroutine(RenewHint());
        isActive = true;
    }

    public void DeactivateBrain()
    {
        StopCoroutine(RenewHint());
        isActive = false;
    }

    Vector3 CalculateRandomPosAroundPlayer()
    {
        Vector3 playerTrans = Player.GetInstance().transform.position;
        return new Vector3(Random.Range(playerTrans.x - range, playerTrans.x + range), playerTrans.y, Random.Range(playerTrans.z - range, playerTrans.z + range));
    }

    IEnumerator RenewHint()
    {
        yield return new WaitForSeconds(hintDelay);
        hint = CalculateRandomPosAroundPlayer();
        StartCoroutine(RenewHint());
    }
}
