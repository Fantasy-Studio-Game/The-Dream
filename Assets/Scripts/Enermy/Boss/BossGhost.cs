using Assets.Scripts.Enermy.Behavior.ActionBehavior;
using Assets.Scripts.Enermy.Behavior.Attack;
using UnityEngine;

namespace Assets.Scripts.Enermy
{
    public class BossGhost : EnermyController
    {
        public GameObject projectilePrefab;

        private void Awake()
        {
            attackMethod = new BuzzAroundShootAttack();

            actionBehavior = new GhostActionBehavior(distanceView, speed);
        }

        protected override void Launch()
        {
            //int anglesRotate = 0;

            //if (_direction == -1)
            //{
            //    anglesRotate = 180;
            //}

            //GameObject projectileObject = Instantiate(projectilePrefab, _rigidbody2D.position + Vector2.up * 0.23f, Quaternion.Euler(0, 0, anglesRotate));

            //TrunkProjectile projectile = projectileObject.GetComponent<TrunkProjectile>();
            //projectile.Launch(new Vector2(_direction, 0), 100, atk);
        }
    }
}
