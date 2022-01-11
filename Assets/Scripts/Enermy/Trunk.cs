using Assets.Scripts.Enermy.Action;

public class Trunk : EnermyController
{
    private void Awake()
    {
        attackMethod = new ShootAttack();
    }
}
