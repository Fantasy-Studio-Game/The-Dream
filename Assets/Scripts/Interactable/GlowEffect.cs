using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowEffect : MonoBehaviour
{
    // color
    float duration = 1.5f;
    private float t = 0;
    bool isReset = false;
    Renderer render;

    Color color1 = Color.white;
    Color color2 = new Color(2f, 2f, 2f, 1f);

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
}
