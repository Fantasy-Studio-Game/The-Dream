using System;
using UnityEngine;

namespace Assets.Scripts.Enermy.Behavior.ActionBehavior
{
    internal class FollowActionBahavior: IActionBehavior
    {
        private float acceleration;
        private float distanceView;
        private Action startup;

        private Rigidbody2D targetRigid2d;

        public bool KeyAttack { get; set; }

        public FollowActionBahavior(float distanceView, float acceleration)
        {
            this.distanceView = distanceView;
            this.acceleration = acceleration;
            KeyAttack = true;
        }

        public FollowActionBahavior(float distanceView, float acceleration, Action startup)
        {
            this.distanceView = distanceView;
            this.acceleration = acceleration;
            this.startup = startup;
            KeyAttack = false;
        }

        public Rigidbody2D TargetRigid2d { private set { targetRigid2d = value; } get { return targetRigid2d; } }

        public void BehaveInContext(int direction, ref float speed, ref Rigidbody2D rigidbody2D, ref Animator animator, Action attack, Action unAttack, Action<bool> moving)
        {
            // follow a target
            if (targetRigid2d != null)
            {
                if (startup == null)
                {
                    // only following and attack always
                    if (Vector2.Distance(rigidbody2D.position, targetRigid2d.position) > 0.5f)
                    {
                        speed += acceleration * Time.deltaTime;

                        moving(true);
                    }

                    attack();
                }
                else
                {
                    // attack when far enough
                    float distance = Vector2.Distance(rigidbody2D.position, targetRigid2d.position);
                    if (distance > 10f)
                    {
                        moving(true);
                    }
                    else
                    {
                        if (IsAwake())
                        {
                            attack();
                        }
                    }

                }
            }
            else
            {
                // find player
                RaycastHit2D detectedPayer = Physics2D.CircleCast(rigidbody2D.position + Vector2.up * 0.1f, distanceView, new Vector2(direction, 0), 0f, LayerMask.GetMask("Player"));
                if (detectedPayer.collider != null)
                {
                    targetRigid2d = detectedPayer.rigidbody;

                    // start enermy only once
                    animator.SetTrigger("Awake"); // --> run

                    if (startup != null)
                    {
                        startup.Invoke();
                    }
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
            return KeyAttack && targetRigid2d != null;
        }


    }
}
