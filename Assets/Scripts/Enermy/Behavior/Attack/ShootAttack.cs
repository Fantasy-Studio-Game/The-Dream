
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
            _timerShootCast = _maxTimerShootCast = maxTimerShootCast;

            _maxSpeed = speed;
        }
        public void Attack(ref float speed, ref Animator animator, Action launch)
        {
            _curSpeed = speed = 0;

            animator.SetBool("Attack", true); // --> Attack
            
            if (_timerShootCast > 0)
            {
                _timerShootCast -= Time.deltaTime;

                if ((!_alreadyShoot) && (_timerShootCast > 0.44f))
                {
                    launch();
                    _alreadyShoot = true;
                }
            }
            else
            {
                _timerShootCast = _maxTimerShootCast;

                _alreadyShoot = false;
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
