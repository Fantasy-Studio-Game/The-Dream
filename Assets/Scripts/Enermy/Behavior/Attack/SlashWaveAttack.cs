
using System;
using UnityEngine;

namespace Assets.Scripts.Enermy.Behavior
{
    public class SlashWaveAttack : IAttack
    {
        private bool isAttatck = false;

        public SlashWaveAttack()
        {

        }
        public void Attack(ref float speed, ref Animator animator, Action launch)
        {

            animator.SetBool("Attack", true); // --> Attack

            launch();
        }

        public void UnAttack(ref float speed, ref Animator animator)
        {
            return;
        }

        public bool IsAttacking()
        {
            return isAttatck;
        }

    }
}
