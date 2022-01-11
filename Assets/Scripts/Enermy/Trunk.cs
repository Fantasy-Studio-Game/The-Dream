using Assets.Scripts.Enermy.Behavior;

public class Trunk : EnermyController
{
    private void Awake()
    {
        attackMethod = new ShootAttack();
        (attackMethod as ShootAttack).TimerAttackCast = timerAttackCast;
    }
}
