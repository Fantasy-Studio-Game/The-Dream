using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleHeart : MonoBehaviour
{
    bool isTriggered = false;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTriggered == true)
        {
            return;
        }
        Debug.Log("Trigger");
        PlayerController controller = collision.GetComponent<PlayerController>();
        if (controller != null)
        {
            if (controller.hearts < controller.maxHearts)
            {
                isTriggered = true;

                controller.AddHeart(1);
                Destroy(gameObject);

            }
            else if (controller.hearts == controller.maxHearts)
            {
                if (controller.health < controller.maxHealth)
                {
                    isTriggered = true;

                    controller.ChangeHealth(controller.maxHealth);
                    Destroy(gameObject);

                }
            }
        }
    }
}
