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

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
    }

  public void StartGame()
    {
        SceneManager.LoadScene(start);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

   public void LoadOptions()
    {
        SceneManager.LoadScene(options);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(menu);
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }
 
}
