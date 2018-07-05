using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private static CameraFollow cameraInstance;
    //Public
    public Transform targetPlayer;
    public float smoothing = 5f;
    public float moveSpeed = 5;
    public float camSwingSpeed = 15f;
    public float camRotSpeed = 90f;
    public Vector3 offset;
    public bool border;
    private bool hasOtherTarget = false; 
    public float minX, maxX,minZ, maxZ;
    private Quaternion startRot; 

    private void Awake()
    {
        if (cameraInstance == null)
            cameraInstance = this;
        startRot = transform.rotation;
    }
    public static CameraFollow GetInstance()
    {
        return cameraInstance;
    }

    void FixedUpdate()
    {
        if (border)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, Mathf.Clamp(transform.position.z,minZ,maxZ));
        }

        if (Player.GetInstance() && !hasOtherTarget)
        {
            
            targetPlayer = Player.GetInstance().transform;
            Vector3 targetCamPos = targetPlayer.position + offset;
            transform.rotation = startRot;
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

        }
    }
    
    public void HasOtherTarget(bool otherTarget)
    {
        hasOtherTarget = otherTarget;
    }

    public bool Move(Vector3 position, Quaternion rotation)
    {
        transform.position = Vector3.MoveTowards(transform.position, position, camSwingSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, camRotSpeed * Time.deltaTime);
        if (transform.position == position)
            return true;
        return false; 
    }
}
