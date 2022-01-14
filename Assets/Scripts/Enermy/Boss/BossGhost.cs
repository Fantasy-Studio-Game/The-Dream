using Assets.Scripts.Enermy.Behavior.ActionBehavior;
using Assets.Scripts.Enermy.Behavior.Attack;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enermy
{
    public class BossGhost : EnermyController
    {
        public float rateActiveRange = 3f;
        public float appearTimer = 3.20f;
        public float delayTransition = 1f;
        public float castSpell = 5f;

        public GameObject projectilePrefab;

        private CapsuleCollider2D capsuleCollider2D;
        private Vector2 originCollierSize;

        private void Awake()
        {
            capsuleCollider2D = GetComponent<CapsuleCollider2D>();
            originCollierSize = capsuleCollider2D.size;
            capsuleCollider2D.size = originCollierSize * rateActiveRange;

            GetComponent<SpriteRenderer>().enabled = false;

            attackMethod = new BuzzAroundShootAttack();

            actionBehavior = new GhostActionBehavior(delayTransition, appearTimer, castSpell);
        }

        protected override void Launch()
        {
            Debug.Log("Launch");

            //int anglesRotate = 0;

            //if (_direction == -1)
            //{
            //    anglesRotate = 180;
            //}

            //GameObject projectileObject = Instantiate(projectilePrefab, _rigidbody2D.position + Vector2.up * 0.23f, Quaternion.Euler(0, 0, anglesRotate));

            //TrunkProjectile projectile = projectileObject.GetComponent<TrunkProjectile>();
            //projectile.Launch(new Vector2(_direction, 0), 100, atk);
        }

        protected override void Moving(bool canMove)
        {
            Debug.Log("New position");

            //base.Moving(canMove);
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            PlayerController controller = collision.gameObject.GetComponent<PlayerController>();

            if (controller != null)
            {
                if (actionBehavior.IsAwake())
                {
                    base.OnCollisionEnter2D (collision);
                }
                else
                {
                    _animator.SetTrigger("Awake");
                    GetComponent<SpriteRenderer>().enabled = true;
                    //capsuleCollider2D.size = originCollierSize; // change after appear effect
                }
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            OnCollisionEnter2D(collision);
        }

    }
}
