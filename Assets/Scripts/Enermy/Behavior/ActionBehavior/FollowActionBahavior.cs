﻿using System;
using UnityEngine;

namespace Assets.Scripts.Enermy.Behavior.ActionBehavior
{
    internal class FollowActionBahavior: IActionBehavior
    {
        private float acceleration;
        private float distanceView;
        private Action startup;

        private Rigidbody2D targetRigid2d;

        public FollowActionBahavior(float distanceView, float acceleration)
        {
            this.distanceView = distanceView;
            this.acceleration = acceleration;
        }

        public FollowActionBahavior(float distanceView, float acceleration, Action startup)
        {
            this.distanceView = distanceView;
            this.acceleration = acceleration;
            this.startup = startup;
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

                    moving(true);
                }

                attack();
            }
            else
            {
                // find player
                RaycastHit2D detectedPayer = Physics2D.CircleCast(rigidbody2D.position + Vector2.up * 0.1f, 1.5f, new Vector2(direction, 0), distanceView, LayerMask.GetMask("Player"));
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
            return targetRigid2d != null;
        }


    }
}
