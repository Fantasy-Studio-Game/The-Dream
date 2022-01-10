
using UnityEngine;

namespace Assets.Scripts.Enermy.Action
{
    public interface IAttack
    {
        void Attack(ref float timerFireCast, ref float speed, ref bool alreadyShoot, ref Animator animator);
    }
}
