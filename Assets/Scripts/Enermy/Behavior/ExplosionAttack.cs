using System;
using UnityEngine;

namespace Assets.Scripts.Enermy.Behavior
{
    public class ExplosionAttack : IAttack
    {
        float _maxSpeed;

        public float MaxSpeed { set { _maxSpeed = value; } }
        public void Attack(ref float timerAttackCast, ref float speed, ref Animator animator)
        {
            animator.SetBool("Attack", true); // --> Attack
            speed = _maxSpeed * 2.5f;
        }

        public bool IsAttacking(ref float timerAttackCast, ref float speed, Action launch)
        {
            // This enermy always moves

            if (timerAttackCast < 0)
            {
                speed = _maxSpeed;
            }
            else
            {
                timerAttackCast -= Time.deltaTime;
            }

            return false;
        }
    }
}
