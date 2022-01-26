using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject activatedCheckpoint;
    public GameObject unActivatedCheckpoint;
    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        activatedCheckpoint.SetActive(false);
        unActivatedCheckpoint.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision);
        PlayerController controller = collision.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.SetCheckpoint(transform.position);
            controller.PlayOneShotAudio(audioClip);
            activatedCheckpoint.SetActive(true);
            unActivatedCheckpoint.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
