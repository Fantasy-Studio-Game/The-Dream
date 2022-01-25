using Assets.Scripts.Enermy.Behavior;
using Assets.Scripts.Enermy.Behavior.ActionBehavior;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBoss : EnermyController
{
    private void Awake()
    {
        attackMethod = new GoreAttack(speed, 0);

        actionBehavior = new FollowActionBahavior(distanceView, 0);
    }
}
