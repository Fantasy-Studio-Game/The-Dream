
using System;
using UnityEngine;

namespace Assets.Scripts.Enermy.Behavior
{
    public class ShootAttack : IAttack
    {
        private float _maxTimerShootCast;
        private float _timerShootCast;
        private bool _alreadyShoot;

        public ShootAttack(float maxTimerShootCast)
        {
            _maxTimerShootCast = maxTimerShootCast;
        }
        public void Attack(ref float speed, ref Animator animator, Action launch)
        {
            //timerAttackCast = _maxTimerShootCast;
            //speed = 0;
            //_alreadyShoot = false;
            //animator.SetBool("Attack", true); // --> Attack

            //if (_timerShootCast > 0)
            //{
            //    _timerShootCast -= Time.deltaTime;

            //    if (!_alreadyShoot && (_timerShootCast - timerAttackCast > 0.6))
            //    {
            //        launch();
            //        _alreadyShoot = true;

            //    }

            //    return true;
            //}
        }

        public bool IsAttacking()
        {
            return _timerShootCast < _maxTimerShootCast;
        }
    }
}
