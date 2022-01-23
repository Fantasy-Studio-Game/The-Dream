using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public bool isSettingOpen = false;

    ///////// UI ///////
    public Toggle FullScreenToggle;
    public Slider MusicSlider;
    public Slider SFXSlider;
    public Dropdown qualityDropDown;
    string[] qualities;
    public Dropdown resolutionDropDown;
    Resolution[] resolutions;

    ///////// Audio ////////
    public AudioMixer audioMixer;
    float originalMusicVolume;
    float musicVolume;
    float originalSFXVolume;
    float sfxVolume;

    ////// Quality and Resolution///////
    int currentQualityIndex;
    bool currentFullScreen;
    int currentResolutionIndex;

    // Start is called before the first frame update
    void Start()
    {
        float audioValue = 0f;
        bool isGettingValue = false;

        //Get Music Volume;
        isGettingValue = audioMixer.GetFloat("MusicVolume", out audioValue);
        if (isGettingValue) 
        {
            originalMusicVolume = audioValue;
        }
        else 
        {
            originalMusicVolume = 0f;
        }
        MusicSlider.value = originalMusicVolume;
        

        //Get SFX Volume;
        isGettingValue = audioMixer.GetFloat("SFXVolume", out audioValue);
        if (isGettingValue) 
        {
            originalSFXVolume = audioValue;
        }
        else 
        {
            originalSFXVolume = 0f;
        }
        SFXSlider.value = originalSFXVolume;

        // Get Resolution
        resolutions = Screen.resolutions;
        resolutionDropDown.ClearOptions();
        List<string> optionsResolution = new List<string>();
        for (int i = resolutions.Length - 1; i >= 0; i--) {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            optionsResolution.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height) {
                currentResolutionIndex = resolutions.Length - i - 1;
            }
        }
        resolutionDropDown.AddOptions(optionsResolution);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();

        // Get Graphic
        qualities = QualitySettings.names;
        qualityDropDown.ClearOptions();
        List<string> optionsQuality = new List<string>();
        for (int i = 0; i < qualities.Length; i++) {
            string option = qualities[i];
            optionsQuality.Add(option);
        }
        currentQualityIndex = QualitySettings.GetQualityLevel();
        qualityDropDown.AddOptions(optionsQuality);
        qualityDropDown.value = currentQualityIndex;
        qualityDropDown.RefreshShownValue();

        //Get FullScreen
        currentFullScreen = Screen.fullScreen;
        FullScreenToggle.isOn = currentFullScreen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMusicVolume(float volume) 
    {
        audioMixer.SetFloat("MusicVolume", volume);
        musicVolume = volume;
    }

    public void ChangeSFXVolume(float volume) 
    {
        audioMixer.SetFloat("SFXVolume", volume);
        sfxVolume = volume;
    }

    public void SetQuality(int qualityIndex) 
    {
        //QualitySettings.SetQualityLevel(qualityIndex);
        currentQualityIndex = qualityIndex;
    }

    public void SetFullScreen(bool isFullScreen) 
    {
        //Screen.fullScreen = isFullScreen;
        currentFullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex) 
    {        
        //Resolution resolution = resolutions[resolutionIndex];
        //Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        currentResolutionIndex = resolutions.Length - resolutionIndex - 1;
        //Debug.Log(currentResolutionIndex);
    }
}
