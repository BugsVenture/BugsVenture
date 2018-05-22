using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    //Public
    public Transform pauseMenu;
    public Transform audioMenu;
    public GameObject EventSystem1;
    public GameObject EventSystem2;


    // Use this for initialization
    void Start ()
    {
        EventSystem1.gameObject.SetActive(true);
        EventSystem2.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
        audioMenu.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
       if (Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            if (pauseMenu.gameObject.activeInHierarchy == false)
            {
                pauseMenu.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                pauseMenu.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.gameObject.activeInHierarchy == false)
            {
                pauseMenu.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                pauseMenu.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    //Audio Settings
    public void AudioSettings()
    {
        if (audioMenu.gameObject.activeInHierarchy == false)
        {
            audioMenu.gameObject.SetActive(true);
            pauseMenu.gameObject.SetActive(false);
            EventSystem1.gameObject.SetActive(false);
            EventSystem2.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            EventSystem1.gameObject.SetActive(true);
            EventSystem2.gameObject.SetActive(false);
            pauseMenu.gameObject.SetActive(true);
            audioMenu.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
    }

    //Resume
    public void Resume()
    {
        EventSystem1.gameObject.SetActive(true);
        EventSystem2.gameObject.SetActive(false);
        audioMenu.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
