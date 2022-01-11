
using UnityEngine;

namespace Assets.Scripts.Enermy.Action
{
    public class ShootAttack : IAttack
    {
        float _timerFireCast = 2f;
        public void Attack(ref float timerFireCast, ref float speed, ref bool alreadyShoot, ref Animator animator)
        {
            timerFireCast = _timerFireCast;
            speed = 0;
            alreadyShoot = false;
            animator.SetBool("Attack", true); // --> Attack
        }
    }
}
