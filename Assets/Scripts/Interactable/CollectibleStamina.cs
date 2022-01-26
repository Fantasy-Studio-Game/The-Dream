using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleStamina : GlowEffect
{
    public float time = 5;
    bool isTriggered = false;

    public AudioClip audioClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTriggered == true)
        {
            return;
        }
        PlayerController controller = collision.GetComponent<PlayerController>();
        if (controller != null)
        {
            isTriggered = true;

            controller.AddStamina(time);
            controller.PlayCollectingAudio(audioClip);
            Destroy(gameObject);
        }
    }
}
