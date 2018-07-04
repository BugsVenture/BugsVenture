using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTarget : MonoBehaviour
{
    //Public
    public Transform target;
    public float smooth = 0.3f;
    public Vector3 offset;

    //Private
    private CameraFollow camfolow;
    // Use this for initialization
    void Start ()
    {
        camfolow = GetComponent<CameraFollow>();
    }
	
	// Update is called once per frame
	void Update ()
    {

    }


   public void switchTarget(Transform target)
    {
        Vector3 targetPos = target.position;
        Vector3 cameraPos = this.transform.position;
        camfolow.enabled = !camfolow.enabled;
        this.transform.position = Vector3.Lerp(cameraPos, targetPos + offset, 1f);
    }

}
