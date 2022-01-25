using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    ///////// UI /////////
    public GameObject settingMenu;
    public GameObject startMenu;


    public void PlayGame() {
        startMenu.SetActive(true);
    }
    public void LoadGame () {

    }
    public void SettingsGame () {
        settingMenu.SetActive(true);
    }
    public void ExitGame () {
        Application.Quit();
    }
}
