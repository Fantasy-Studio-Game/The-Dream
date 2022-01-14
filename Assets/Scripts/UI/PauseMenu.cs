using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPause = false;
    public GameObject pauseMenuUI;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPause) {
                Resume();
            }
            else {
                Pause();
            }
        }
    }
    public void Resume () {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
    }
    void Pause () {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPause = true;
    }
    public void Load () {
        Time.timeScale = 1f;
    }
    public void Mainmenu () {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
    public void Options () {
        
    }
    public void Exit () {
        Application.Quit();
    }
}
