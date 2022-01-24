using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPause = false;
    ///////// UI /////////
    public GameObject pauseMenuUI;
    public GameObject settingMenu;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPause) {
                ResumeGame();
            }
            else {
                PauseGame();
            }
        }
    }
    public void ResumeGame () {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
    }
    void PauseGame () {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPause = true;
    }
    public void LoadGame () {

    }
    public void SaveGame () {

    }
    public void Mainmenu () {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
    public void SettingGame () {
        settingMenu.SetActive(true);
    }
    public void ExitGame () {
        Application.Quit();
    }
}
