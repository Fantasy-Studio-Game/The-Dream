using Assets.Scripts.Enermy.Behavior.ActionBehavior;
using Assets.Scripts.Enermy.Behavior.Attack;
using UnityEngine;

namespace Assets.Scripts.Enermy
{
    public class BossGhost : EnermyController
    {
        private void Awake()
        {
            attackMethod = new BuzzAroundShootAttack();

            actionBehavior = new GhostActionBehavior(distanceView, speed);
        }
    }
}
