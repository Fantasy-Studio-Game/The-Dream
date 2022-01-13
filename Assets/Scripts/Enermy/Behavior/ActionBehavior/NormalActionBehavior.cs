using System;
using UnityEngine;

namespace Assets.Scripts.Enermy.Behavior.ActionBehavior
{
    public class NormalActionBehavior: IActionBehavior
    {
        private float distanceView;
        private float speed;
        private bool isAwake = false;

        public NormalActionBehavior(float distanceView, float speed)
        {
            this.distanceView = distanceView;
            this.speed = speed;
        }
        public void BehaveInContext(int direction, ref float speed, Rigidbody2D rigidbody2D, ref Animator animator, Action attack, Action<bool> moving)
        {
            RaycastHit2D detectedPayer = Physics2D.Raycast(rigidbody2D.position + Vector2.up * 0.1f, new Vector2(direction, 0), distanceView, LayerMask.GetMask("Player"));
            if (detectedPayer.collider != null)
            {
                if (isAwake)
                {
                    //attackMethod.Attack(ref _timerAttackCast, ref _speed, ref _isShoot, ref _animator);
                    attack();
                }
                else
                {
                    // start enermy only once
                    animator.SetTrigger("Awake"); // --> Move

                    // run run
                    isAwake = true;
                    speed = this.speed;
                }

            }

            moving(isAwake);
        }
    }
}
