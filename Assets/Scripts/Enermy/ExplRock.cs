
using Assets.Scripts.Enermy.Behavior;
public class ExplRock : EnermyController
{
    private void Awake()
    {
        attackMethod = new ExplosionAttack();
        (attackMethod as ExplosionAttack).MaxSpeed = speed / 2;
    }
}
