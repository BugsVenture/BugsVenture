using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectControllInput : MonoBehaviour
{
    public CharacterMovement player;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Detect Mouse Input
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
            player.useController = false;
        }
        if (Input.GetAxisRaw("Mouse X") != 0.0f || Input.GetAxisRaw("Mouse Y") != 0.0f)
        {
            player.useController = false;
        }

        //Detect Controller Input
        if (Input.GetAxisRaw("HorizontalJ") != 0.0f || Input.GetAxisRaw("VerticalJ") != 0.0f)
        {
            player.useController = true;
        }
    }
}

