using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public GameObject LevelText;
    public void MouseOn() {
        LevelText.SetActive(true);
    }
    public void MouseOff() {
        LevelText.SetActive(false);
    }
}
