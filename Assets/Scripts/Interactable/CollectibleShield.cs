using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleShield : MonoBehaviour
{
    float duration = 1.5f;
    private float t = 0;
    bool isReset = false;
    Renderer render;

    Color color1 = Color.white;
    Color color2 = new Color(0.7f, 1f, 5, 1);

    void Start()
    {
        render = this.GetComponent<Renderer>();
    }
    void Update()
    {
        ColorChanger();
    }


    void ColorChanger()
    {
        render.material.color = Color.Lerp(color1, color2, t);
        if (t < 1)
        {
            t += Time.deltaTime / duration;
        }
        else
        {
            t = 0;
            Color temp = color1;
            color1 = color2;
            color2 = temp;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(11);
        PlayerController controller = collision.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.AddShield(1);
            Destroy(this.gameObject);
        }
    }
}
