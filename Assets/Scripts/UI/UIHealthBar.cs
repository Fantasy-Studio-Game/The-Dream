using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public static UIHealthBar instance { get; set; }
    public Image mask;
    float originalSize;
    // Start is called before the first frame update
    private void Awake()
    {
        originalSize = mask.rectTransform.rect.width;
        instance = this;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
