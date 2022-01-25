using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public GameObject PreviousMenu;

    public void BackToPreviousMenu() {
        PreviousMenu.SetActive(true);
    }

    public void Level1OnClick() {
        SceneManager.LoadScene("Level 1");
    }

    public void Level2OnClick() {
        SceneManager.LoadScene("Level 2");
    }
    public void Level3OnClick() {
        SceneManager.LoadScene("Level 3");
    }
}
