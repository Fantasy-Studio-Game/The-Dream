using System;
using UnityEngine;

namespace Assets.Scripts.Enermy.Behavior
{
    public class ExplosionAttack : IAttack
    {
        public ExplosionAttack()
        {
        }

        public void Attack(ref float speed, ref Animator animator, Action launch)
        {
            //if (!_isExplosion)
            //{
            //    _isExplosion = true;
            //    boxCollider2D.offset = new Vector2(0, 0.1f);
            //    boxCollider2D.size = new Vector2(1.5f, 0.8f);

            //    _animator.SetBool("Attack", true); // --> Attack
            //}
            //else
            //{
            //    if (_explosionTime < 0)
            //    {
            //        Destroy(gameObject);
            //    }
            //    else
            //    {
            //        _explosionTime -= Time.deltaTime;
            //    }
            //}
        }

        public void UnAttack(ref float speed, ref Animator animator)
        {
            //
        }

        public bool IsAttacking()
        {
            // This enermy always moves
            return false;
        }
    }
}
