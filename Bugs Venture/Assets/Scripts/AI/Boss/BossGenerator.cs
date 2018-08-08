using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGenerator : MonoBehaviour
{

    public GameObject darkArea;

    public GameObject CamPos;

    public bool isLast = false;

    private bool isDestroyed = false;

    private bool inDestruction = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Boss")
        {
            BossEnemy bEnemy = collision.gameObject.GetComponent<BossEnemy>();
            bEnemy.GetDamage();
            inDestruction = true;
        }
    }
    void Update()
    {
        if (inDestruction)
        {
            Destroy();
        }
    }

    void Destroy()
    {
        if (!isDestroyed)
        {
            if (isLast)
            {
                CameraFollow cam = CameraFollow.GetInstance();
                cam.HasOtherTarget(true);
                if (cam.Move(CamPos.transform.position, CamPos.transform.rotation))
                {
                    cam.HasOtherTarget(false);
                    inDestruction = false;
                    isDestroyed = true;
                }
                return;
            }
            DarkArea dArea = darkArea.GetComponent<DarkArea>();
            dArea.SetActive();
            isDestroyed = true;
        }
    }
}
