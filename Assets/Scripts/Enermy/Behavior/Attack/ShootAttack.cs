
using System;
using UnityEngine;

namespace Assets.Scripts.Enermy.Behavior
{
    public class ShootAttack : IAttack
    {
        private float _maxTimerShootCast;
        private float _timerShootCast;

        private float _maxSpeed;
        private float _curSpeed;

        private bool _alreadyShoot;

        public ShootAttack(float maxTimerShootCast, float speed)
        {
            _maxTimerShootCast = maxTimerShootCast;

            _maxSpeed = speed;
        }
        public void Attack(ref float speed, ref Animator animator, Action launch)
        {
            _timerShootCast = _maxTimerShootCast;

            _curSpeed = speed = 0;

            _alreadyShoot = false;

            animator.SetBool("Attack", true); // --> Attack

            if (_timerShootCast > 0)
            {
                _timerShootCast -= Time.deltaTime;

                if (!_alreadyShoot && (_timerShootCast - _timerShootCast > 0.6))
                {
                    launch();
                    _alreadyShoot = true;
                }
            }
        }

        public void UnAttack(ref float speed, ref Animator animator)
        {
            speed = _maxSpeed;
        }

        public bool IsAttacking()
        {
            return _curSpeed <= 0;
        }
    }
}
