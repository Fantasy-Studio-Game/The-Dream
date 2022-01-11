
using Assets.Scripts.Enermy.Behavior;
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
}
