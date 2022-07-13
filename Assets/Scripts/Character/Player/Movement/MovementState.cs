using StarterAssets;
using UnityEngine;

namespace Player.Movement
{
    public abstract class MovementState : State<MovementStateMachine>
    {
        protected PlayerController player;

        protected StarterAssetsInputs inputs;
        protected Camera mainCamera;
        protected CharacterMovement movement;
        protected Animator animator;
        protected Ragdoll ragdoll;

        public MovementState(MovementStateMachine context) : base(context)
        {
            player = context.Player;
            movement = context.Movement;
            inputs = context.Inputs;
            mainCamera = context.MainCamera;
            animator = player.Animator;
            ragdoll = context.Ragdoll;
        }
    }
}