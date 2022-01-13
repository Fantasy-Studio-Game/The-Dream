using Assets.Scripts.Enermy.Behavior;
using Assets.Scripts.Enermy.Behavior.ActionBehavior;

public class Rino : EnermyController
{
    private void Awake()
    {
        attackMethod = new GoreAttack();
        (attackMethod as GoreAttack).MaxSpeed = speed;

        actionBehavior = new NormalActionBehavior(distanceView, speed);
    }
    protected override void Launch()
    {
        // do nothing
    }
}
