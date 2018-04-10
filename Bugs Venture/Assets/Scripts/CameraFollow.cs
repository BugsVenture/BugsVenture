using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Public
    public Transform target;
    public float smoothing = 5f;
    public float moveSpeed = 5;

    Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

        transform.Translate(Input.GetAxis("Right Analog Vertical") * Vector3.up * Time.deltaTime * moveSpeed);
        transform.Translate(Input.GetAxis("Right Analog Horizontal") * Vector3.right * Time.deltaTime * moveSpeed);
    }
}
