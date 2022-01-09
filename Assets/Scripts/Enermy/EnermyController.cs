using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyController : MonoBehaviour
{
    public bool isCanFire = false;
    public float speed = 1.0f;
    public float distance = 5.0f;

    private Rigidbody2D _rigidbody2D;
    private int _direction = 1;
    private float _distance;

    // Trigger function
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _distance = distance / 2;
    }

    void FixedUpdate()
    {
        Moving();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    // general function
    void Moving()
    {
        _distance -= Time.deltaTime * speed;

        if (_distance < 0)
        {
            _direction = -_direction;
            _distance = distance;
        }

        Vector2 position = _rigidbody2D.position;
        position.x = position.x + Time.deltaTime * speed * _direction;

        _rigidbody2D.MovePosition(position);
    }


}
