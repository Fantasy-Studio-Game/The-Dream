using System;
using UnityEngine;

namespace Assets.Scripts.Enermy.Behavior.ActionBehavior
{
    public interface IActionBehavior
    {
        /// <summary>
        /// Define behavior for context
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="speed"></param>
        /// <param name="rigidbody2D"></param>
        /// <param name="animator"></param>
        /// <param name="attack"></param>
        /// <param name="moving"></param>
        void BehaveInContext(int direction, ref float speed, ref Rigidbody2D rigidbody2D, ref Animator animator, Action attack, Action unAttack, Action<bool> moving);

        /// <summary>
        /// Check if Enermy is awake or not
        /// </summary>
        /// <returns>bool</returns>
        bool IsAwake();
    }
}
