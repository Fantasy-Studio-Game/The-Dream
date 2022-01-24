using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    ///////// UI ///////
    public GameObject PreviousMenu;
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
    int originalQualityIndex;
    int currentQualityIndex;

    bool originalFullScreen;
    bool currentFullScreen;

    int originalResolutionIndex;
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
                originalResolutionIndex = currentResolutionIndex;
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
        originalQualityIndex = currentQualityIndex;
        qualityDropDown.AddOptions(optionsQuality);
        qualityDropDown.value = currentQualityIndex;
        qualityDropDown.RefreshShownValue();

        //Get FullScreen
        currentFullScreen = Screen.fullScreen;
        originalFullScreen = currentFullScreen;
        FullScreenToggle.isOn = currentFullScreen;
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
        currentQualityIndex = qualityIndex;
    }

    public void SetFullScreen(bool isFullScreen) 
    {
        currentFullScreen = isFullScreen;
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex) 
    {        
        currentResolutionIndex = resolutionIndex;
    }

    public void BackToPreviousMenu() {
        ///Reset volume
        audioMixer.SetFloat("MusicVolume", originalMusicVolume);
        audioMixer.SetFloat("SFXVolume", originalSFXVolume);

        ///Reset value
        musicVolume = originalMusicVolume;
        MusicSlider.value = originalMusicVolume;

        sfxVolume = originalSFXVolume;
        SFXSlider.value = originalSFXVolume;

        currentResolutionIndex = originalResolutionIndex;
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();

        currentQualityIndex = originalQualityIndex;
        qualityDropDown.value = currentQualityIndex;
        qualityDropDown.RefreshShownValue();

        currentFullScreen = originalFullScreen;
        FullScreenToggle.isOn = currentFullScreen;

        PreviousMenu.SetActive(true);
    }

    public void ApplySetting() {
        //// Save Volume
        originalMusicVolume = musicVolume;
        originalSFXVolume = sfxVolume;

        //// Save Quality
        QualitySettings.SetQualityLevel(currentQualityIndex);
        originalQualityIndex = currentQualityIndex;

        //// Save Full Screen
        Screen.fullScreen = currentFullScreen;
        originalFullScreen = currentFullScreen;

        //// Save Resolution
        Resolution resolution = resolutions[resolutions.Length - currentResolutionIndex - 1];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        originalResolutionIndex = currentResolutionIndex;
    }
}
