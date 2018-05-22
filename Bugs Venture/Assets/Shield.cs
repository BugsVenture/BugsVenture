using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float speed;

	// Update is called once per frame
	void Update ()
    {
        this.transform.Rotate(Vector3.right * Time.deltaTime * speed);
	}
}
