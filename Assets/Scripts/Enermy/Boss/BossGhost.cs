using Assets.Scripts.Enermy.Behavior.ActionBehavior;
using Assets.Scripts.Enermy.Behavior.Attack;
using Assets.Scripts.Helper;
using UnityEngine;

namespace Assets.Scripts.Enermy
{
    public class BossGhost : EnermyController
    {
        public float rateActiveRange = 3f;
        public float appearTimer = 3.20f;
        public float delayTransition = 1f;
        public float castSpell = 5f;
        public float distanceAppearFromPlayer = 5f;

        public GameObject projectilePrefab;

        private CapsuleCollider2D capsuleCollider2D;
        private Vector2 originCollierSize;
        private Vector2 targetCollierVector;
        private Rigidbody2D playerRigid;


        private float _maxAngleCastBullet = 270f;
        private float _aAngleCastBullet = 15f;

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
            //float numberBullet = _maxAngleCastBullet / _aAngleCastBullet;
            //for (float angle = 0f; angle < numberBullet; angle += _aAngleCastBullet)
            //{

            //    //targetCollierVector.Normalize();

            //    //float anglesRotate = Vector2.SignedAngle(new Vector2(0, 0), targetCollierVector);


            //    GameObject projectileObject = Instantiate(projectilePrefab, _rigidbody2D.position + Vector2.up * 0.23f, Quaternion.Euler(0, 0, angle));

            //    BossGhostProjectile projectile = projectileObject.GetComponent<BossGhostProjectile>();
            //    projectile.Launch(angle.DegreeToVector2(), 100, atk);

            //}

            GameObject projectileObject = Instantiate(projectilePrefab, _rigidbody2D.position + Vector2.up * 0.23f, Quaternion.Euler(0, 0, _aAngleCastBullet * 3));

            BossGhostProjectile projectile = projectileObject.GetComponent<BossGhostProjectile>();
            projectile.Launch((_aAngleCastBullet * 3).DegreeToVector2(), 100, atk);
        }

        protected override void Moving(bool canMove)
        {
            float posX = Random.Range(-distanceAppearFromPlayer, distanceAppearFromPlayer);
            float posY = Mathf.Sqrt(Mathf.Pow(distanceAppearFromPlayer, 2) - Mathf.Pow(posX, 2)) * (Random.Range(0, 1) * 2 - 1);

            targetCollierVector = new Vector2(posX, posY);

            _rigidbody2D.MovePosition(targetCollierVector + playerRigid.position);

            if (targetCollierVector.x < 0)
            {
                _animator.SetFloat("Horizontal", 1);
            }
            else
            {
                _animator.SetFloat("Horizontal", -1);
            }
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
                    (actionBehavior as GhostActionBehavior).Active();

                    playerRigid = controller.GetComponent<Rigidbody2D>();
                    capsuleCollider2D.size = originCollierSize; // change after appear effect
                }
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            OnCollisionEnter2D(collision);
        }

    }
}
