using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    //public AudioClip otherClip;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ChangeBGMusic(AudioClip otherAudio) {
        if (audioSource.isPlaying)
        {
            audioSource.Stop ();
            audioSource.loop = true;
            audioSource.clip = otherAudio;
            audioSource.Play();
        }
    }
}
