using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play() {
        SceneManager.LoadScene("Level 1");
    }
    public void Load () {
        
    }
    public void Options () {
        
    }
    public void Exit () {
        Application.Quit();
    }
}
