using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb2d;
    private Vector2 originPosition;
    int type;
    // Start is called before the first frame update
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        originPosition = rb2d.position;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(rb2d.position, originPosition) > 10)
        {
            Destroy(gameObject);
        }

    }

    public void Launch(Vector2 direction, float force)
    {
        rb2d.AddForce(direction * force);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        EnermyController enemy = other.GetComponent<EnermyController>();
        if (enemy != null)
        {
            Debug.Log("Hit enemy: ", enemy);
        }
        Destroy(gameObject);
    }
}
