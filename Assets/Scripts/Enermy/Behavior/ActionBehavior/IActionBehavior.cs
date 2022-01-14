using System;
using UnityEngine;

namespace Assets.Scripts.Enermy.Behavior.ActionBehavior
{
    public interface IActionBehavior
    {
        void BehaveInContext(int direction, ref float speed, ref Rigidbody2D rigidbody2D, ref Animator animator, Action attack, Action<bool> moving);
        bool IsAwake();
    }
}
