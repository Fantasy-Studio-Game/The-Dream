using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleHealthPotion : GlowEffect
{
    public int amount = 10;
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
            if (controller.health < controller.maxHealth)
            {
                isTriggered = true;

                controller.ChangeHealth(amount);
                Destroy(gameObject);

            }
        }
    }
}
