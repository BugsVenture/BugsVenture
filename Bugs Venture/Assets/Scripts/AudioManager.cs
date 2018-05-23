using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Slider Volume;
    public AudioSource Music;

    void Update()
    {
        Music.volume = Volume.value;
    }
    
}
