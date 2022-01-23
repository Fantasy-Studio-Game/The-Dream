using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    ///////// UI /////////
    public GameObject settingMenu;

    public void PlayGame() {
        SceneManager.LoadScene("Level 1");
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
