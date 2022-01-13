using System;
using UnityEngine;

namespace Assets.Scripts.Enermy.Behavior.ActionBehavior
{
    public class GhostActionBehavior : IActionBehavior
    {
        private float distanceView;
        private float appearTimer;
        private bool isAwake = false;

        public GhostActionBehavior(float distanceView, float appearTimer)
        {
            this.distanceView = distanceView;
            this.appearTimer = appearTimer;
        }
        public void BehaveInContext(int direction, ref float speed, Rigidbody2D rigidbody2D, ref Animator animator, Action attack, Action<bool> moving)
        {
            if (appearTimer <= 0)
            {
                isAwake = true;

                //RaycastHit2D detectedPayer = Physics2D.Raycast(rigidbody2D.position + Vector2.up * 0.1f, new Vector2(direction, 0), distanceView, LayerMask.GetMask("Player"));
                //if (detectedPayer.collider != null)
                //{
                //    if (isAwake)
                //    {
                //        attack();
                //    }
                //    else
                //    {
                //        // start enermy only once
                //         // --> Move

                //        // run run
                //        isAwake = true;
                //        speed = this.speed;
                //    }

                //}

                //moving(isAwake);
            }
            else
            {
                appearTimer -= Time.deltaTime;
            }
        }

        public bool IsAwake()
        {
            return isAwake;
        }
    }
}
