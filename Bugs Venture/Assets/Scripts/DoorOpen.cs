using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    //Public
    public GameObject Door;

    public bool isClosed = false;

    private bool isOpen = false;

    public Vector3 openOffset = Vector3.zero;

    public float openSpeed = 1; 

    public int terminalsNeeded = 2;

    private int terminalCount = 0;

    public Transform destTrans;

    private bool playCameraRide = false;

    private AudioSource aSource;

    private Vector3 offset;

    private Vector3 startPos; 

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
        startPos = Door.transform.position;
        aSource = GetComponent<AudioSource>();
        offset = Door.transform.position + openOffset; 
    }

    private void FixedUpdate()
    {
        if(isOpen)
        {
            Door.transform.position = Vector3.Lerp(Door.transform.position, offset, openSpeed);
        }
        else
        {
            Door.transform.position = Vector3.Lerp(Door.transform.position, startPos, openSpeed);
        }
        if (playCameraRide)
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

    void OnTriggerEnter(Collider other)
    {
        if (!isClosed)
        {
            if ((other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy"))
            {
                isOpen = true;
                aSource.Play();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(!isClosed)
        {
            isOpen = false; 
            aSource.Play();
        }
    }
}
