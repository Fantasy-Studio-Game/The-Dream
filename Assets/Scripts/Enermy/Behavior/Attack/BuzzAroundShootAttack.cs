using System;
using UnityEngine;

namespace Assets.Scripts.Enermy.Behavior.Attack
{
    public class BuzzAroundShootAttack : IAttack
    {
        private Action fire;

        public void Attack(ref float timerAttackCast, ref float speed, ref Animator animator)
        {
            if (fire != null)
            {
                fire();
            }
        }

        public bool IsAttacking(ref float timerAttackCast, ref float speed, Action launch)
        {
            if (fire == null)
            {
                fire = launch;
            }

            return false;
        }
    }
}
