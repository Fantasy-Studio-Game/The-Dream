using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void Mainmenu () {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
}
