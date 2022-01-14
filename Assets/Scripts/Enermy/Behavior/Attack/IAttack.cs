
using System;
using UnityEngine;

namespace Assets.Scripts.Enermy.Behavior
{
    public interface IAttack
    {
        /// <summary>
        /// Check timer to launch
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="animator"></param>
        /// <param name="launch"></param>
        void Attack(ref float speed, ref Animator animator, Action launch);

        /// <summary>
        /// Return if IAttack is running
        /// </summary>
        /// <returns></returns>
        bool IsAttacking();
    }
}
