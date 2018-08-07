using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    //Public
    public GameObject Door;

    public bool isClosed = false;

    public int terminalsNeeded = 2;

    public Transform destTrans;
    public GameObject doorSmoke;
    public Transform smokePos;

    //Private
    private int terminalCount = 0;
    private bool isCreated = false;

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
                if(!isCreated)
                {
                GameObject smoke = Instantiate(doorSmoke, smokePos.transform.position, Quaternion.Euler(new Vector3(0, 180, 0)));
                isCreated = true;
                Destroy(smoke, 0.5f);
                }
            }
        }
    }
}
