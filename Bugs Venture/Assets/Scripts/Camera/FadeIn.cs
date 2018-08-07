using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    //Public
    public Image fadeInSprite;

    private void Start()
    {
        //fadeInSprite.enabled = false;
    }

    public void startFadingIn()
    {
        fadeInSprite.CrossFadeAlpha(1, 0.5f, false);
    }
    public void startFadingOut()
    {
        fadeInSprite.CrossFadeAlpha(0, 0.5f, false);
    }

}
