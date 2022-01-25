using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowStrike : MonoBehaviour
{
    Rigidbody2D rb2d;
    private Vector2 originPosition;
    // Start is called before the first frame update
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        originPosition = rb2d.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

    }

    // Update is called once per frame
    void Update()
    {

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
            enemy.GetDamage(30);
            Destroy(gameObject);
        }

        TrunkProjectile projectile = other.GetComponent<TrunkProjectile>();
        if (projectile != null)
        {
            Destroy(projectile.gameObject);
        }
    }
}
