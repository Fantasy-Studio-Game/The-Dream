using System;
using UnityEngine;

namespace Assets.Scripts.Enermy.Behavior.ActionBehavior
{
    internal class FollowDefenceActionBehavior : IActionBehavior
    {
        private float distanceView;
        private Action startup;
        private float attackRange;

        private Transform attackPoint;
        private LayerMask playerProjectileMask;

        private Rigidbody2D targetRigid2d;

        public bool KeyAttack { get; set; }

        public FollowDefenceActionBehavior(float distanceView, float attackRange, Transform attackPoint, LayerMask playerProjectileMask, Action startup)
        {
            this.distanceView = distanceView;
            this.attackRange = attackRange;
            this.attackPoint = attackPoint;
            this.playerProjectileMask = playerProjectileMask;
            this.startup = startup;

            KeyAttack = false;
        }

        public Rigidbody2D TargetRigid2d { private set { targetRigid2d = value; } get { return targetRigid2d; } }

        public void BehaveInContext(int direction, ref float speed, ref Rigidbody2D rigidbody2D, ref Animator animator, Action attack, Action unAttack, Action<bool> moving)
        {
            // follow a target
            if (targetRigid2d != null)
            {
                // detected any projectile from player
                Collider2D[] detectedProjectiles = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerProjectileMask);
                if (detectedProjectiles.Length >= 1)
                {
                    animator.SetTrigger("Defence");
                }

                if (Vector2.Distance(rigidbody2D.position, targetRigid2d.position) < distanceView / 2)
                {
                    attack();
                }
                else
                {
                    moving(true);
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

        public bool IsAwake()
        {
            return KeyAttack && targetRigid2d != null;
        }


    }
}
