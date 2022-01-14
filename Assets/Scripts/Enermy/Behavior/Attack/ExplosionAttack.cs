using System;
using UnityEngine;

namespace Assets.Scripts.Enermy.Behavior
{
    public class ExplosionAttack : IAttack
    {
        private BoxCollider2D _collider;
        private float _countdown;

        private bool _isExplosion = false;
        public ExplosionAttack(ref BoxCollider2D collider2D, float countdown)
        {
            _collider = collider2D;
            _countdown = countdown;
        }

        public void Attack(ref float speed, ref Animator animator, Action launch)
        {
            if (!_isExplosion && _countdown <= 0)
            {
                _collider.offset = new Vector2(0, 0.1f);
                _collider.size = new Vector2(1f, 1f);

                animator.SetBool("Attack", true); // --> Attack

                // make sure this block run once
                _isExplosion = true;
            }
            
            _countdown -= Time.deltaTime;
        }

        public void UnAttack(ref float speed, ref Animator animator)
        {
            // this action can not cancel...
        }

        public bool IsAttacking()
        {
            // This enermy always moves
            return true;
        }

        public bool IsExplosion()
        {
            return _countdown <= -1f;
        }
    }
}
