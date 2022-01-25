using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapProjectTile : MonoBehaviour
{
    public float rangeBullet = 5.0f;

    private Rigidbody2D _rigidbody2D;
    private Vector2 _originPosition;
    private Animator _animator;
    private float _time_destroy = 0.5f;
    private int _atk = 10;


    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _originPosition = _rigidbody2D.position;

        //_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(_rigidbody2D.position, _originPosition) > rangeBullet)
        {
            StartCoroutine(DestroyProjectTile());
        }
    }

    private IEnumerator DestroyProjectTile()
    {
        //_animator.SetTrigger("Destroy");

        yield return new WaitForSeconds(_time_destroy);

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.ChangeHealth(-_atk);
        }

        StartCoroutine(DestroyProjectTile());
    }

    public void Launch(Vector2 direction, float force, int atk)
    {
        _rigidbody2D.AddForce(direction * force);
        _atk = atk;
    }
}
