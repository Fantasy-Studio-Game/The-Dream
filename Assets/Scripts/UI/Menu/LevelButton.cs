using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    /////// Audio ///////
    public AudioClip clickAudio;
    public AudioClip hoverAudio;
    public AudioSource audioSource;

    /////// UI ///////
    public GameObject LevelText;


    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    public void Click() {
        audioSource.PlayOneShot(clickAudio);
    }
    public void MouseOn() {
        audioSource.PlayOneShot(hoverAudio);
        LevelText.SetActive(true);
    }
    public void MouseOff() {
        LevelText.SetActive(false);
    }
}
