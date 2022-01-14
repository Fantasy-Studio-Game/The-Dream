using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishScene : MonoBehaviour
{
    public void Home() {
        SceneManager.LoadScene("Main Menu");
    }
}
