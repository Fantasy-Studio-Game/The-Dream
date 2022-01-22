using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMusicVolume(float volume) 
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void ChangeSFXVolume(float volume) 
    {
        audioMixer.SetFloat("SFXVolume", volume);
    }
}
