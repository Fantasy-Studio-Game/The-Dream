using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIShield : MonoBehaviour
{
    public static UIShield instance { get; set; }
    TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetText(string str)
    {
        text.SetText(str);
    }

    public void SetValue(int num)
    {
        this.SetText(num.ToString());
        if (num < 1)
        {
            text.color = Color.red;
        }
        else
        {
            text.color = Color.white;
        }
    }
}
