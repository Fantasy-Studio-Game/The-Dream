
using Assets.Scripts.Enermy.Behavior;
using Assets.Scripts.Enermy.Behavior.ActionBehavior;
using UnityEngine;

public class ExplRock : EnermyController
{
    public float acceleration = 0.01f;

    private BoxCollider2D boxCollider2D;
    private bool _isExplosion = false;
    private void Awake()
    {
        attackMethod = new ExplosionAttack();
        boxCollider2D = GetComponent<BoxCollider2D>();

        actionBehavior = new FollowActionBahavior(distanceView, speed);

    }    

    // general function
    protected override void Moving(bool canMove)
    {
        if (canMove)
        {
            //explosionTimeCast -= Time.deltaTime;

            //if (_distanceMove < 0)
            //{
            //    attackMethod.Attack(ref timerAttackCast, ref _speed, ref _animator);
            //    return;
            //}

            Vector2 position = _rigidbody2D.position;
            position.x = position.x + Time.deltaTime * _speed * _direction;

            _animator.SetFloat("Horizontal", _direction);

            _rigidbody2D.MovePosition(position);


        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        // follow player and............
        // :)) explode bummmmmmmm

        // start up
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            (actionBehavior as FollowActionBahavior).startUpbytouch(collision.gameObject.GetComponent<Rigidbody2D>());
            _animator.SetTrigger("Awake");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollisionEnter2D(collision);
    }
}
