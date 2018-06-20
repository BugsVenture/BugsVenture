using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenExitDoor : MonoBehaviour
{
    //Public
    public GameObject ExitDoor;
    public int terminalCount = 0;

	// Update is called once per frame
	void Update ()
    {
		if(terminalCount == 2)
        {
            this.ExitDoor.GetComponent<Animation>().Play();
            StartCoroutine(Delay());
        }
	}


    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.8f);
        this.ExitDoor.GetComponent<Animation>().Stop();
        terminalCount = 0;
    }
}
