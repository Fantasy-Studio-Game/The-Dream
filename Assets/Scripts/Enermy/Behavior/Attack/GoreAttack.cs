using System;
using UnityEngine;

namespace Assets.Scripts.Enermy.Behavior
{
    public class GoreAttack : IAttack
    {
        private float _maxSpeed;
        private float _speed;

        private float _maxAngryTimer;
        private float _angryTimer;

        public GoreAttack(float maxSpeed, float maxAngryTimer)
        {
            _maxSpeed = maxSpeed;
            _speed = 0;

            _maxAngryTimer = maxAngryTimer;
            _angryTimer = 0;
        }
        public void Attack(ref float speed, ref Animator animator, Action launch)
        {
            _speed = speed;
            speed = _maxSpeed * 2.5f;

            _angryTimer = _maxAngryTimer;

            animator.SetBool("Attack", true);
        }

        public void UnAttack(ref float speed, ref Animator animator)
        {
            if (_angryTimer < 0)
            {
                _speed = speed = _maxSpeed;
                animator.SetBool("Attack", false);
            }
            else
            {
                _angryTimer -= Time.deltaTime;
            }
        }

        public bool IsAttacking()
        {
            return Mathf.Approximately(_speed, _maxSpeed);
        }
    }
}
