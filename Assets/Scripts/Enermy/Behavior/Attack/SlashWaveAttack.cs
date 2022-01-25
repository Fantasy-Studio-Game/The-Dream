
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
            launch();            
            
            isAttatck = true;
        }

        public void UnAttack(ref float speed, ref Animator animator)
        {
            isAttatck = false;
        }

        public bool IsAttacking()
        {
            return isAttatck;
        }

    }
}
