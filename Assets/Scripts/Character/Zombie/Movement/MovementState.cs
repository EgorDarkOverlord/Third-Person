using UnityEngine;

namespace Zombie.Movement
{
    public abstract class MovementState : State<MovementStateMachine>
    {
        protected ZombieController zombie;

        protected Transform player;
        protected Animator animator;
        protected ZombieMovment movement;
        protected Ragdoll ragdoll;



        public MovementState(MovementStateMachine context) : base(context)
        {
            zombie = context.Zombie;
            player = context.Player;
            animator = context.Animator;
            movement = context.Movement;
            ragdoll = context.Ragdoll;
        }
    }
}
