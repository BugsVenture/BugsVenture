using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneController : MonoBehaviour
{
    //Public
    public string start;
    public string options;
    public string menu;
   [SerializeField]
    public string lastLevel;
    public SaveGame sv;
    [SerializeField]
    public int Buttonactive;
    public GameObject ContinueButton;
    public GameObject Question;
    public GameObject GreyLayer;
    public GameObject Controlls;
    public GameObject Settings;
    public GameObject Credits;

    void Start()
    {
        sv = GetComponent<SaveGame>();
        if(Application.loadedLevelName == "Menu")
        {
            lastLevel = PlayerPrefs.GetString("lastLevel", lastLevel);
            Buttonactive = PlayerPrefs.GetInt("Buttonactive", Buttonactive);
            if (Buttonactive == 1)
            {
                ContinueButton.SetActive(true);
            }

        }
            if (Application.loadedLevelName == "Project_Test")
            {
                Buttonactive = 1;
                PlayerPrefs.SetInt("Buttonactive", Buttonactive);
            }
    }

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

    // Continue Game
    public void Continue()
    {
        SceneManager.LoadScene(lastLevel);
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
        PlayerPrefs.SetString("lastLevel", lastLevel);
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

    //Disable the Question Window
    public void QuestionDisable()
    {
        Question.SetActive(false);
        Question.GetComponent<Animation>().Play();
        GreyLayer.SetActive(false);
    }
    //Enable the Question Window
    public void QuestionEnable()
    {
        Question.SetActive(true);
        GreyLayer.SetActive(true);
    }
    //Enable the Controlls Window
    public void ControllsEndable()
    {
        Controlls.SetActive(true);
        Controlls.GetComponent<Animation>().Play();
        GreyLayer.SetActive(true);
    }
    //Disable the Controlls Window 
    public void ControllsDisable()
    {
        Controlls.SetActive(false);
        GreyLayer.SetActive(false);
    }
    //Enable the Settings Window
    public void SettingsEnable()
    {
        Settings.SetActive(true);
        Settings.GetComponent<Animation>().Play();
        GreyLayer.SetActive(true);
    }
    //Disable the Settings Window
    public void SettingsDisable()
    {
        Settings.SetActive(false);
        GreyLayer.SetActive(false);
    }
    //Enable the Credits Window
    public void CreditsEnable()
    {
        Credits.SetActive(true);
        GreyLayer.SetActive(true);
    }
    //Disable the Credits Window
    public void CreditsDisable()
    {
        Credits.SetActive(false);
        GreyLayer.SetActive(false);
    }

}
