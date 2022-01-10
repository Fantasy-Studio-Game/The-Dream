using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaksController : MonoBehaviour
{

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (_spriteRenderer.sprite.name == "peaks_3")
        {
            // this sprite does not have peaks
            return;
        }

        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null)
        {
            controller.ChangeHealth(-1);
        }
    }
}
