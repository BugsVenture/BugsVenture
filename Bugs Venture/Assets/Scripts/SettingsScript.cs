using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    //Public
    public Transform settings;
   
    //Private
    Event keyEvent;
    Text buttonText;
    KeyCode newKey;
    bool waitingForKey;


    // Use this for initialization
    void Start()
    {
        waitingForKey = false;

        for (int i = 0; i < 5; i++)
        {
            if (settings.GetChild(i).name == "Teleport Keyboard")
            {
                settings.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.teleportKey.ToString();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        keyEvent = Event.current;

        if(keyEvent.isKey && waitingForKey)
        {
            newKey = keyEvent.keyCode;
            waitingForKey = false;
        }
    }

    public void StartAssignment(string keyName)
    {
        if(!waitingForKey)
        {
            StartCoroutine(AssignKey(keyName));
        }
    }

    public void SendText(Text text)
    {
        buttonText = text;
    }

    IEnumerator WaitForKey()
    {
        while(!keyEvent.isKey)
        {
            yield return null;
        }
    }

    public IEnumerator AssignKey(string keyName)
    {
        waitingForKey = true;

        yield return WaitForKey();

        switch(keyName)
        {
            case "TeleportKey":
                GameManager.GM.teleportKey = newKey;
                buttonText.text = GameManager.GM.teleportKey.ToString();
                PlayerPrefs.SetString("TeleportKey", GameManager.GM.teleportKey.ToString());
                break;
        }
        yield return null;
    }
}
