using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void Resume () {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
    }
    void Pause () {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPause = true;
    }
}
