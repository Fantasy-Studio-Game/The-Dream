
using Assets.Scripts.Enermy.Behavior;
using Assets.Scripts.Enermy.Behavior.ActionBehavior;
using UnityEngine;

public class ExplRock : EnermyController
{
    private BoxCollider2D boxCollider2D;
    private bool _isExplosion = false;
    private float _explosionTime = 0.8f;
    private void Awake()
    {
        attackMethod = new ExplosionAttack(speed/2, timerAttackCast);
        boxCollider2D = GetComponent<BoxCollider2D>();

        actionBehavior = new FollowActionBahavior(distanceView, speed);
    }    

    // general function

    protected override void Launch()
    {
        if (!_isExplosion)
        {
            _isExplosion = true;
            boxCollider2D.offset = new Vector2(0, 0.1f);
            boxCollider2D.size = new Vector2(1.5f, 0.8f);

            _animator.SetBool("Attack", true); // --> Attack
        }
        else
        {
            if (_explosionTime < 0)
            {
                Destroy(gameObject);
            }
            else
            {
                _explosionTime -= Time.deltaTime;
            }
        }
    }

    protected override void Moving(bool canMove)
    {
        if (canMove)
        {
            _distanceMove -= Time.deltaTime * _speed;

            if (_distanceMove < 0)
            {
                _direction = -_direction;
                _distanceMove = distanceMove;
            }

            Vector2 position = _rigidbody2D.position;
            position.x = position.x + Time.deltaTime * _speed * _direction;

            _animator.SetFloat("Horizontal", _direction);

            _rigidbody2D.MovePosition(position);

            if (_speed == 0)
            {
                _speed = speed;
            }

        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        // follow player and............
        // :)) explode bummmmmmmm
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollisionEnter2D(collision);
    }
}
