using System;
using UnityEngine;

namespace Assets.Scripts.Enermy.Behavior
{
    public class ExplosionAttack : IAttack
    {
        float _maxSpeed;
        float _maxTimerAttackCast;
        bool _allin = false;
        public ExplosionAttack(float maxSpeed, float timerAttackCast)
        {
            _maxSpeed = maxSpeed;
            _maxTimerAttackCast = timerAttackCast;
        }

        public void Attack(ref float timerAttackCast, ref float speed, ref Animator animator)
        {
            speed = _maxSpeed * 2.5f;
            _allin = true;
        }

        public bool IsAttacking(ref float timerAttackCast, ref float speed, Action launch)
        {
            // This enermy always moves
            if (_allin)
            {
                if (timerAttackCast >= _maxTimerAttackCast)
                {
                    speed = 0;
                    launch();

                    return true;
                }
                else
                {
                    timerAttackCast += Time.deltaTime;
                }
            }

            return false;
        }
    }
}
