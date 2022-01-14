using Assets.Scripts.Enermy.Behavior;
using Assets.Scripts.Enermy.Behavior.ActionBehavior;

public class Rino : EnermyController
{
    public float angryTimer = 5.0f;
    private void Awake()
    {
        attackMethod = new GoreAttack(speed, angryTimer);

        actionBehavior = new NormalActionBehavior(distanceView, speed);
    }
    protected override void Launch()
    {
        // do nothing
    }
}
