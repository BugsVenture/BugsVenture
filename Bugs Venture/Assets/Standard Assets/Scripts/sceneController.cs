using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneController : MonoBehaviour
{
    //Public
    public string start;
    public string options;
    public string menu;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    // Start Game
    public void StartGame()
    {
        SceneManager.LoadScene(start);
    }

    //Exit Game
    public void ExitGame()
    {
        Application.Quit();
    }

    // Load Options
   public void LoadOptions()
    {
        SceneManager.LoadScene(options);
    }

    //Back to Menu
    public void BackToMenu()
    {
        SceneManager.LoadScene(menu);
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    //Restart Level
    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }
 
}
