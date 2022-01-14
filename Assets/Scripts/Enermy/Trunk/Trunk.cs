using Assets.Scripts.Enermy.Behavior;
using Assets.Scripts.Enermy.Behavior.ActionBehavior;
using UnityEngine;

public class Trunk : EnermyController
{
    public float timerAttackCast = 2.0f;
    public GameObject projectilePrefab;

    private void Awake()
    {
        attackMethod = new ShootAttack(timerAttackCast);

        actionBehavior = new NormalActionBehavior(distanceView, speed);
    }

    protected override void Launch()
    {
        int anglesRotate = 0;

        if (_direction == -1)
        {
            anglesRotate = 180;
        }

        GameObject projectileObject = Instantiate(projectilePrefab, _rigidbody2D.position + Vector2.up * 0.23f, Quaternion.Euler(0, 0, anglesRotate));

        TrunkProjectile projectile = projectileObject.GetComponent<TrunkProjectile>();
        projectile.Launch(new Vector2(_direction, 0), 100, atk);
    }
}
