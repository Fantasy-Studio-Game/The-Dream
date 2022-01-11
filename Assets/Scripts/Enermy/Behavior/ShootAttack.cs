
using System;
using UnityEngine;

namespace Assets.Scripts.Enermy.Behavior
{
    public class ShootAttack : IAttack
    {
        float _timerAttackCast;
        bool _alreadyShoot;

        public float TimerAttackCast { set { _timerAttackCast = value; } }
        public void Attack(ref float timerAttackCast, ref float speed, ref Animator animator)
        {
            timerAttackCast = _timerAttackCast;
            speed = 0;
            _alreadyShoot = false;
            animator.SetBool("Attack", true); // --> Attack
        }

        public bool IsAttacking(ref float timerAttackCast, ref float speed, Action launch)
        {
            if (timerAttackCast > 0)
            {
                timerAttackCast -= Time.deltaTime;

                if (!_alreadyShoot && (_timerAttackCast - timerAttackCast > 0.6))
                {
                    launch();
                    _alreadyShoot = true;

                }

                return true;
            }

            return false;
        }
    }
}
