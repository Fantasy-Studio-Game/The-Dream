

using Assets.Scripts.Enermy.Behavior;
using Assets.Scripts.Enermy.Behavior.ActionBehavior;

public class NorRock : EnermyController
{
    private void Awake()
    {
        attackMethod = new GoreAttack();
        (attackMethod as GoreAttack).MaxSpeed = speed / 2;

        actionBehavior = new NormalActionBehavior(distanceView, speed);
    }
    protected override void Launch()
    {
        // do nothing
    }
}
