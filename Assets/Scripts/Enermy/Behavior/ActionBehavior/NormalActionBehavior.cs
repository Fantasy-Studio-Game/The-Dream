using System;
using UnityEngine;

namespace Assets.Scripts.Enermy.Behavior.ActionBehavior
{
    public class NormalActionBehavior : IActionBehavior
    {
        private float distanceView;
        private float speed;
        private bool isAwake = false;
        private Rigidbody2D targetRigid2d;
        public Rigidbody2D TargetRigid2d { private set { targetRigid2d = value; } get { return targetRigid2d; } }


        public NormalActionBehavior(float distanceView, float speed)
        {
            this.distanceView = distanceView;
            this.speed = speed;
        }
        public void BehaveInContext(int direction, ref float speed, ref Rigidbody2D rigidbody2D, ref Animator animator, Action attack, Action unAttack, Action<bool> moving)
        {
            RaycastHit2D detectedPayer = Physics2D.CircleCast(rigidbody2D.position + Vector2.up * 0.1f, distanceView, new Vector2(direction, 0), distanceView, LayerMask.GetMask("Player"));
            targetRigid2d = detectedPayer.rigidbody;

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
            else
            {
                // lost sign of player
                if (isAwake)
                {
                    unAttack();
                }
            }
            moving(isAwake);

        }

        public bool IsAwake()
        {
            return isAwake;
        }
    }
}
