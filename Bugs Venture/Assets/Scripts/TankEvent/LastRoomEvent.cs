using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastRoomEvent : MonoBehaviour
{
    //Public
    public GameObject Light;
    public GameObject characterMovement;
    public CharacterMovement cm;
    public FadeIn fadeInScript;
    public string menu;

    // Use this for initialization
    void Start ()
    {
        characterMovement = GameObject.FindGameObjectWithTag("Player");
        cm = characterMovement.GetComponentInChildren<CharacterMovement>();
        fadeInScript.startFadingOut();
        Light.SetActive(false);
        fadeInScript = FindObjectOfType<FadeIn>();
    }

     void OnTriggerEnter(Collider other)
    {
        cm.enabled = false;
        StartCoroutine(LightDelay());
    }

    IEnumerator LightDelay()
    {
        yield return new WaitForSeconds(1);
        Light.SetActive(true);
        yield return new WaitForSeconds(2);
        fadeInScript.startFadingIn();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(menu);
    }


}
