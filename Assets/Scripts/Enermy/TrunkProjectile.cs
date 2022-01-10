
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkProjectile : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    Vector2 _originPosition;

    public float rangeBullet = 5.0f;
    
    private int _atk = 10;

    //public ParticleSystem hitParticleSystem;

    // Awake is called before the first frame update
    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _originPosition = _rigidbody2D.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(_rigidbody2D.position, _originPosition) > rangeBullet)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ParticleSystem particle = Instantiate(hitParticleSystem, _rigidbody2D.position, Quaternion.identity);
        //particle.Play();

        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.ChangeHealth(-_atk);
        }

        Destroy(gameObject);
    }

    public void Launch(Vector2 direction, float force, int atk)
    {
        _rigidbody2D.AddForce(direction * force);
        _atk = atk;
    }
}