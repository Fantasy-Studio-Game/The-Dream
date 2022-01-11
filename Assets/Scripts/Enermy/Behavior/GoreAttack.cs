using System;
using UnityEngine;

namespace Assets.Scripts.Enermy.Behavior
{
    public class GoreAttack : IAttack
    {
        float _maxSpeed;

        public float MaxSpeed { set { _maxSpeed = value; } }
        public void Attack(ref float timerAttackCast, ref float speed, ref Animator animator)
        {
            speed = _maxSpeed * 2;
            //animator.SetBool("Attack", true); // --> Attack
            if (timerAttackCast < 0)
            {
                speed = _maxSpeed;
            }
            else
            {
                speed = 0;
            }
        }

        public bool IsAttacking(ref float timerAttackCast, ref float speed, Action launch)
        {
            // This enermy always moves
            return false;
        }
    }
}
