using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log(collision);
        PlayerController controller = collision.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.ChangeHealth(-1);
        }
    }
}
