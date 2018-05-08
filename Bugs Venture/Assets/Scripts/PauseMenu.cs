using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public Transform pauseMenu;
    public Transform settingsMenu;
    public Transform eventSystem1;
    public Transform eventSystem2;

    // Use this for initialization
    void Start ()
    {
        pauseMenu.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(false);
        eventSystem1.gameObject.SetActive(true);
        eventSystem2.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
       if (Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            if (pauseMenu.gameObject.activeInHierarchy == false)
            {
                pauseMenu.gameObject.SetActive(true);
                eventSystem1.gameObject.SetActive(true);
                eventSystem2.gameObject.SetActive(false);
                Time.timeScale = 0;
            }
            else
            {
                pauseMenu.gameObject.SetActive(false);
                eventSystem1.gameObject.SetActive(false);
                eventSystem2.gameObject.SetActive(true);
                Time.timeScale = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.gameObject.activeInHierarchy == false)
            {
                pauseMenu.gameObject.SetActive(true);
                eventSystem1.gameObject.SetActive(true);
                eventSystem2.gameObject.SetActive(false);
                Time.timeScale = 0;
            }
            else
            {
                pauseMenu.gameObject.SetActive(false);
                eventSystem1.gameObject.SetActive(false);
                eventSystem2.gameObject.SetActive(true);
                Time.timeScale = 1;
            }
        }
    }

    public void AudioSettings()
    {
        if (settingsMenu.gameObject.activeInHierarchy == false)
        {
            settingsMenu.gameObject.SetActive(true);
            eventSystem1.gameObject.SetActive(false);
            eventSystem2.gameObject.SetActive(true);
            pauseMenu.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.gameObject.SetActive(true);
            eventSystem1.gameObject.SetActive(true);
            eventSystem2.gameObject.SetActive(false);
            settingsMenu.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
    }

    public void Resume()
    {
        settingsMenu.gameObject.SetActive(false);
        eventSystem1.gameObject.SetActive(true);
        eventSystem2.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
