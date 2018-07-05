using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    //Public
    public GameObject Door;

    public bool isClosed = false;

    public int terminalsNeeded = 2;

    private int terminalCount = 0;

    public Transform destTrans;

    private bool playCameraRide = false; 

    public void IncrementCount()
    {
        terminalCount++;
        playCameraRide = true;
        if(terminalCount == terminalsNeeded)
        {
            isClosed = true;
        }
    }

    public void Start()
    {
    }

    private void FixedUpdate()
    {
        if(playCameraRide)
        {
            MoveCamera();
        }
    }

    void MoveCamera()
    {
        CameraFollow.GetInstance().HasOtherTarget(true);
        if(CameraFollow.GetInstance().Move(destTrans.transform.position, destTrans.transform.localRotation))
        {
            StartCoroutine(CameraDelay());
        }
    }

    IEnumerator CameraDelay()
    {
        yield return new WaitForSeconds(1f);
        CameraFollow.GetInstance().HasOtherTarget(false);
        playCameraRide = false;
        isClosed = false; 
    }

    void OnTriggerStay(Collider other)
    {
            Debug.Log("InTrigger");
        if (!isClosed)
        {
            if ((other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy"))
            {
                this.Door.GetComponent<Animation>().Play();
            }
        }
    }

}
