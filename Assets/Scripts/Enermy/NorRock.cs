

using Assets.Scripts.Enermy.Behavior;

public class NorRock : EnermyController
{
    private void Awake()
    {
        attackMethod = new GoreAttack();
        (attackMethod as GoreAttack).MaxSpeed = speed / 2;
    }
}
