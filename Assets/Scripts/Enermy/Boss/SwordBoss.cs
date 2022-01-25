using Assets.Scripts.Enermy.Behavior;
using Assets.Scripts.Enermy.Behavior.ActionBehavior;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBoss : EnermyController
{
    public float castTimer; // 1.5f
    public float castDelay = 0.2f;
    public float appearTimer = 2.0f;

    //private Rigidbody2D teleportationJutsu;
    private int boss_shield;

    private void Awake()
    {
        attackMethod = new SlashWaveAttack();

        actionBehavior = new FollowActionBahavior(distanceView, 0, StartUp);

        boss_shield = shield;
        shield = 100;

    }

    protected override void Launch()
    {
        StartCoroutine(CastWaveAttack());
    }

    private IEnumerator CastWaveAttack()
    {
        _speed = 0;
        yield return new WaitForSeconds(castTimer);

        Vector2 directionVec = (actionBehavior as FollowActionBahavior).TargetRigid2d.position - _rigidbody2D.position;
        directionVec.Normalize();

        _rigidbody2D.AddForce(directionVec * 20);

        yield return new WaitForSeconds(castDelay);

        _speed = speed;
        _animator.SetBool("Attack", false);
    }

    protected override void Moving(bool canMove)
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

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController controller = collision.gameObject.GetComponent<PlayerController>();

        if (controller != null)
        {
            if (actionBehavior.IsAwake())
            {
                controller.ChangeHealth(-atk);
            }
        }
    }

    private void StartUp()
    {
        StartCoroutine(ShieldTimer());
    }

    private IEnumerator ShieldTimer()
    {
        yield return new WaitForSeconds(appearTimer);

        shield = boss_shield;
        _speed = speed;
    }
}
