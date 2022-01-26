using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleHeart : GlowEffect
{
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
            if (controller.hearts < controller.maxHearts)
            {
                isTriggered = true;

                controller.AddHeart(1);
                controller.PlayCollectingAudio(audioClip);
                Destroy(gameObject);

            }
            else if (controller.hearts == controller.maxHearts)
            {
                if (controller.health < controller.maxHealth)
                {
                    isTriggered = true;

                    controller.ChangeHealth(controller.maxHealth);
                    controller.PlayCollectingAudio(audioClip);
                    Destroy(gameObject);

                }
            }
        }
    }
}
