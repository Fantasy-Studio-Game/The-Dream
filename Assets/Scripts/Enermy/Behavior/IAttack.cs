
using System;
using UnityEngine;

namespace Assets.Scripts.Enermy.Behavior
{
    public interface IAttack
    {
        void Attack(ref float timerAttackCast, ref float speed, ref Animator animator);

        bool IsAttacking(ref float timerAttackCast, ref float speed, Action launch);
    }
}
