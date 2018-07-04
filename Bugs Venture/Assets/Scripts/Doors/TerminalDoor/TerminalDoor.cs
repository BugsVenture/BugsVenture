using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalDoor : MonoBehaviour
{
    //Public
    public GameObject Door;
    public GameObject Lamp;
    public Transform target;

    //Private
    private SwitchTarget st;

    void Start()
    {
        st = FindObjectOfType<SwitchTarget>(); ;
    }

    void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            this.Door.GetComponent<Animation>().Play();
            StartCoroutine(StopAnimation());
            Lamp.SetActive(false);
            st.switchTarget(target);
        }
    }
    IEnumerator StopAnimation()
    {
        yield return new WaitForSeconds(1);
        this.Door.GetComponent<Animation>().Stop();
    }

}
