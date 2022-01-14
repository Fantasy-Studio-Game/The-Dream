using System;
using UnityEngine;

namespace Assets.Scripts.Enermy.Behavior.ActionBehavior
{
    internal class FollowActionBahavior: IActionBehavior
    {
        private float acceleration;
        private float distanceView;

        private Rigidbody2D targetRigid2d;

        public FollowActionBahavior(float distanceView, float acceleration)
        {
            this.distanceView = distanceView;
            this.acceleration = acceleration;
        }

        public Rigidbody2D TargetRigid2d { private set { targetRigid2d = value; } get { return targetRigid2d; } }

        public void BehaveInContext(int direction, ref float speed, ref Rigidbody2D rigidbody2D, ref Animator animator, Action attack, Action unAttack, Action<bool> moving)
        {
            // follow a target
            if (targetRigid2d != null)
            {
                if (Vector2.Distance(rigidbody2D.position, targetRigid2d.position) > 0.5f)
                {
                    speed += acceleration * Time.deltaTime;

                    attack();
                    moving(true);
                }
            }
            else
            {
                // find player
                RaycastHit2D detectedPayer = Physics2D.Raycast(rigidbody2D.position + Vector2.up * 0.1f, new Vector2(direction, 0), distanceView, LayerMask.GetMask("Player"));
                if (detectedPayer.collider != null)
                {
                    targetRigid2d = detectedPayer.rigidbody;

                    // start enermy only once
                    animator.SetTrigger("Awake"); // --> run
                }                
            }
        }

        public void startUpbytouch(Rigidbody2D target)
        {
            if (targetRigid2d == null)
            {
                targetRigid2d = target;
            }
            else
            {
                // call attack
            }
        }

        public bool IsAwake()
        {
            return targetRigid2d != null;
        }


    }
}
