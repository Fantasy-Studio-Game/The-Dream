using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleStamina : GlowEffect
{
    public float time = 5;
    bool isTriggered = false;

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
            Destroy(gameObject);
        }
    }
}
