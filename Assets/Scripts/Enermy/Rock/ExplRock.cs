
using Assets.Scripts.Enermy.Behavior;
using Assets.Scripts.Enermy.Behavior.ActionBehavior;
using UnityEngine;

public class ExplRock : EnermyController
{
    public float acceleration = 0.5f;
    public float countDownTimer = 5.0f;

    AudioSource audioSource;

    private void Awake()
    {
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        attackMethod = new ExplosionAttack(ref boxCollider2D, countDownTimer);

        actionBehavior = new FollowActionBahavior(distanceView, acceleration);
        audioSource = GetComponent<AudioSource>();
    }    

    // general function
    protected override void Moving(bool canMove)
    {
        if ((attackMethod as ExplosionAttack).IsDestroy())
        {
            Destroy(gameObject);
        }

        if (canMove)
        {
            Vector2 directionVec = (actionBehavior as FollowActionBahavior).TargetRigid2d.position - _rigidbody2D.position;
            directionVec.Normalize();

            Vector2 position = _rigidbody2D.position;

            position.x = position.x + Time.deltaTime * _speed * directionVec.x;
            position.y = position.y + Time.deltaTime * _speed * directionVec.y;

            if (directionVec.x > 0)
            {
                _direction = 1;
            }
            else
            {
                _direction = -1;
            }

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
            if ((attackMethod as ExplosionAttack).IsExplosion())
            {
                collision.gameObject.GetComponent<PlayerController>().ChangeHealth(-atk);
                audioSource.Play();
            }

            (actionBehavior as FollowActionBahavior).startUpbytouch(collision.gameObject.GetComponent<Rigidbody2D>());
            _animator.SetTrigger("Awake");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollisionEnter2D(collision);
    }
}
