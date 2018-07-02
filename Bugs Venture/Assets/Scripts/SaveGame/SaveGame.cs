using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGame : MonoBehaviour
{
    public sceneController sc;

    void Awake()
    {
        sc.lastLevel = SceneManager.GetActiveScene().name;
        sc = GetComponent<sceneController>();

        if (Application.loadedLevelName == "Project_Test")
        {
            sc.Buttonactive = +1;
            PlayerPrefs.SetInt("Buttonactive", sc.Buttonactive);
        }
    }
}
