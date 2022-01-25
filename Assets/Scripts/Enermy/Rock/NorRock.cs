using System.Collections;
using UnityEngine;
using Assets.Scripts.Enermy.Behavior;
using Assets.Scripts.Enermy.Behavior.ActionBehavior;

public class NorRock : EnermyController
{
    public float angryTimer = 5.0f;
    Vector2 directionVec;
    Vector2 lastDirectionVec;
    Vector2 _originPosition;
    bool isAllowChase = true;
    private void Awake()
    {
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        attackMethod = new GoreAttack(speed / 2, angryTimer);
        actionBehavior = new NormalActionBehavior(distanceView, speed);
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        _originPosition = rigidbody.position;
    }
    protected override void Launch()
    {
        // do nothing
    }

    protected override void Moving(bool canMove)
    {
        if (canMove)
        {
            if (Vector3.Distance(_rigidbody2D.position, _originPosition) < 7f)
            {
                if (isAllowChase == true && (actionBehavior as NormalActionBehavior).TargetRigid2d != null)
                {
                    directionVec = (actionBehavior as NormalActionBehavior).TargetRigid2d.position - _rigidbody2D.position;
                }
            }
            else
            {
                StartCoroutine(StopChase());
                directionVec = _originPosition - _rigidbody2D.position;
            }
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

    IEnumerator StopChase()
    {
        isAllowChase = false;
        yield return new WaitForSeconds(5f);
        isAllowChase = true;
    }

    public override void GetDamage(int damage)
    {
        StartCoroutine(DetectAttacker());
        actionBehavior = new NormalActionBehavior(distanceView, speed);
        _health -= damage * (100 - shield) / 100;
        this.GetComponentInChildren<EnemyHealthBar>()?.SetValue((float)_health / maxHealth);
        _animator.SetTrigger("Hit");

    }

    IEnumerator DetectAttacker()
    {
        distanceView += 3f;
        yield return new WaitForSeconds(0.5f);
        distanceView -= 3f;
    }
}
