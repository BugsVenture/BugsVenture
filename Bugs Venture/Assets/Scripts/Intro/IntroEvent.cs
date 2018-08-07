using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroEvent : MonoBehaviour
{
    public CameraShake CameraShake;
    public FadeIn fadeInScript;
    public string start;
    public GameObject player;
    public GameObject Tank;
    public GameObject TankDestroy;
    public GameObject Alarm;
    public GameObject Explosion;


	// Use this for initialization
	void Start ()
    {
        Explosion.SetActive(false);
        TankDestroy.SetActive(false);
        CameraShake = FindObjectOfType<CameraShake>();
        fadeInScript = FindObjectOfType<FadeIn>();
        PlayIntro();
	}
	
	// Update is called once per frame
	void Update ()
    {

	}


    void PlayIntro()
    {
        //player.enabled = false;
        player.SetActive(false);
        StartCoroutine(Intro());
    }

    IEnumerator Intro()
    {
        //player.SetActive(false);
        //yield return new WaitForSeconds(0.7f);
        fadeInScript.startFadingOut();
        yield return new WaitForSeconds(2.0f);
        CameraShake.ShakeCam();
        Explosion.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        //Sound
        fadeInScript.startFadingIn();
        yield return new WaitForSeconds(2.5f);
        Tank.SetActive(false);
        TankDestroy.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        //Alarm Sound
        Alarm.SetActive(true);
        player.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        fadeInScript.startFadingOut();
        yield return new WaitForSeconds(6.5f);
        fadeInScript.startFadingIn();
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene(start);
    }
}
