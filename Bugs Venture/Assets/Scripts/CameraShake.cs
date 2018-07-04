using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float power = 5f;
    public float duration = 3.0f;
    public Transform camera;

    Vector3 startPosition;

    // Use this for initialization
    void Start()
    {
        camera = Camera.main.transform;
        startPosition = camera.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ShakeCam();
        }
    }

    //Shake Camera when hit a Wall or Enmemy
    public void ShakeCam()
    {
        camera.localPosition = camera.localPosition + Random.insideUnitSphere * power;
        StartCoroutine(CamShake());
    }

    //Knocks the Camera back when shoots
    public void CamKnockBack()
    {
        camera.localPosition = camera.localPosition + new Vector3(0, 0, -2) * power;
        StartCoroutine(CamShake());
    }

    IEnumerator CamShake()
    {
        yield return new WaitForSeconds(duration);
        camera.localPosition = camera.localPosition;
    }
}
