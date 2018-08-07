using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeUiColor : MonoBehaviour
{
    //Public
    public Image image;

	// Use this for initialization
	void Start ()
    {
        image = GetComponent<Image>();
	}

    public void BlinkUI()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        image.color = new Color32(255, 0, 0, 100);
        yield return new WaitForSeconds(0.4f);
        image.color = new Color32(255, 255, 255, 100);
    }
}
