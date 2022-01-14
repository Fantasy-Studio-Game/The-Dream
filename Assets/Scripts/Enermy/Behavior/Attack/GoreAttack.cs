using System;
using UnityEngine;

namespace Assets.Scripts.Enermy.Behavior
{
    public class GoreAttack : IAttack
    {
        private float _maxSpeed;
        private float _speed;

        public float MaxSpeed { set { _maxSpeed = value; } }
        public void Attack(ref float speed, ref Animator animator, Action launch)
        {
            _speed = speed;
            speed = _maxSpeed * 2.5f;
        }

        public bool IsAttacking()
        {
            return _speed == _maxSpeed;
        }
    }
}
