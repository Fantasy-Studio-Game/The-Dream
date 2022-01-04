using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallScript : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    ;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // call decrease health of character
        Destroy(gameObject);
    }

    public void Respawn(Vector2 direction, float force)
    {
        _rigidbody.AddForce(direction * force);
    }
}
