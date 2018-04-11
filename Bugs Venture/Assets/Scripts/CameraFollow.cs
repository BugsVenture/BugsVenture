using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private static CameraFollow cameraInstance;
    //Public
    public Transform target;
    public float smoothing = 5f;
    public float moveSpeed = 5;

    public Vector3 offset;

    private void Awake()
    {
        if (cameraInstance == null)
            cameraInstance = this;
    }
    public static CameraFollow GetInstance()
    {
        return cameraInstance;
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (Player.GetInstance())
        {
            target = Player.GetInstance().transform;
            Vector3 targetCamPos = target.position + offset;

            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

            transform.Translate(Input.GetAxis("Right Analog Vertical") * Vector3.up * Time.deltaTime * moveSpeed);
            transform.Translate(Input.GetAxis("Right Analog Horizontal") * Vector3.right * Time.deltaTime * moveSpeed);
        }
    }
}
