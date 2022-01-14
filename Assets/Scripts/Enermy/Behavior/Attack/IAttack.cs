
using System;
using UnityEngine;

namespace Assets.Scripts.Enermy.Behavior
{
    public interface IAttack
    {
        /// <summary>
        /// Check timer to launch a attack
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="animator"></param>
        /// <param name="launch"></param>
        void Attack(ref float speed, ref Animator animator, Action launch);

        /// <summary>
        /// Cancel Attack
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="animator"></param>
        /// <param name="launch"></param>
        void UnAttack(ref float speed, ref Animator animator);

        /// <summary>
        /// Return true if Enermy is casting spell
        /// </summary>
        /// <returns></returns>
        bool IsAttacking();
    }
}
