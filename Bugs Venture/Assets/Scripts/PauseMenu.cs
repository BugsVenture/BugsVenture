using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public Transform pauseMenu;
    public Transform audioMenu;

    // Use this for initialization
    void Start ()
    {
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

    public void AudioSettings()
    {
        if (audioMenu.gameObject.activeInHierarchy == false)
        {
            audioMenu.gameObject.SetActive(true);
            pauseMenu.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.gameObject.SetActive(true);
            audioMenu.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
    }

    public void Resume()
    {
        audioMenu.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
