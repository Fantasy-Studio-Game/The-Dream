using Assets.Scripts.Enermy.Behavior;
using Assets.Scripts.Enermy.Behavior.ActionBehavior;
using UnityEngine;

public class Trunk : EnermyController
{
    public float timerAttackCast = 1.0f;
    public GameObject projectilePrefab;
    Vector2 directionVec;

    private void Awake()
    {
        attackMethod = new ShootAttack(timerAttackCast, speed);

        actionBehavior = new NormalActionBehavior(distanceView, speed);
    }

    protected override void Launch()
    {
        if ((actionBehavior as NormalActionBehavior).TargetRigid2d != null)
        {
            directionVec = (actionBehavior as NormalActionBehavior).TargetRigid2d.position - _rigidbody2D.position;
            Quaternion rotation = Quaternion.Euler(0.0F, 0.0F, Mathf.Atan2(directionVec.y, directionVec.x) * Mathf.Rad2Deg);
            if (directionVec.x > 0)
            {
                _direction = 1;
            }
            else
            {
                _direction = -1;
            }

            GameObject projectileObject = Instantiate(projectilePrefab, _rigidbody2D.position + Vector2.up * 0.23f, rotation);

            TrunkProjectile projectile = projectileObject.GetComponent<TrunkProjectile>();
            projectile.Launch(directionVec.normalized, 100, atk);
        }
    }
}
