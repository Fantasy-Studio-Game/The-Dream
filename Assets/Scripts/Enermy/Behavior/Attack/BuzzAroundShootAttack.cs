using System;
using UnityEngine;

namespace Assets.Scripts.Enermy.Behavior.Attack
{
    public class BuzzAroundShootAttack : IAttack
    {
        public void Attack(ref float speed, ref Animator animator, Action launch)
        {
            launch();
        }

        public bool IsAttacking()
        {
            return false;
        }
    }
}
