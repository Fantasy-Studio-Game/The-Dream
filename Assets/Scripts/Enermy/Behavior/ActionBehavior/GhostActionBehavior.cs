using System;
using UnityEngine;

namespace Assets.Scripts.Enermy.Behavior.ActionBehavior
{
    public class GhostActionBehavior : IActionBehavior
    {
        private float maxDelayTransition;
        private float appearTimer;
        private float castSpell;
        private bool isAwake = false;

        private bool isActive = false;

        private IGhostState state;

        public GhostActionBehavior(float delayTransition, float appearTimer, float castSpell)
        {
            maxDelayTransition = delayTransition;
            this.appearTimer = appearTimer;
            this.castSpell = castSpell;

            state = new AttackGhostState();
        }
        public void BehaveInContext(int direction, ref float speed, ref Rigidbody2D rigidbody2D, ref Animator animator, Action attack, Action<bool> moving)
        {
            if (isActive)
            {
                // do 1 time per state
                if (appearTimer <= 0)
                {
                    if (isAwake)
                    {
                        appearTimer = state.act(ref animator, maxDelayTransition, castSpell, attack, moving);
                        state = state.getNext();
                    }
                    else
                    {
                        isAwake = true;
                    }
                }
                else
                {
                    appearTimer -= Time.deltaTime;
                }
            }
        }

        public bool IsAwake()
        {
            return isAwake;
        }

        public void Active()
        {
            isActive = true;
        }
    }


    public interface IGhostState
    {
        IGhostState getNext();
        float act(ref Animator animator, float delayTransition, float castSpell, Action attack, Action<bool> moving);
    }

    public class AppearGhostState : IGhostState
    {
        public float act(ref Animator animator, float delayTransition, float castSpell, Action attack, Action<bool> moving)
        {
            animator.SetTrigger("Respawn");
            moving(true);

            return delayTransition;
        }

        public IGhostState getNext()
        {
            return new AttackGhostState();
        }
    }

    public class AttackGhostState : IGhostState
    {
        public float act(ref Animator animator, float delayTransition, float castSpell, Action attack, Action<bool> moving)
        {
            animator.SetBool("Attack", true);
            animator.ResetTrigger("Respawn");
            attack();

            return castSpell;
        }

        public IGhostState getNext()
        {
            return new DisappearGhostState();
        }
    }

    public class DisappearGhostState : IGhostState
    {
        public float act(ref Animator animator, float delayTransition, float castSpell, Action attack, Action<bool> moving)
        {
            animator.SetBool("Attack", false);

            return delayTransition;
        }

        public IGhostState getNext()
        {
            return new AppearGhostState();
        }
    }
}


