using Assets.Scripts.Enermy.Behavior;

public class Rino : EnermyController
{
    private void Awake()
    {
        attackMethod = new GoreAttack();
        (attackMethod as GoreAttack).MaxSpeed = speed;
    }
}
