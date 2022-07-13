using UnityEngine;

namespace Player.Movement
{
    public class ReloadState : MovementState
    {
        public ReloadState(MovementStateMachine context) : base(context)
        {
        }

        public override void Enter()
        {
            movement.Jump = false;
            movement.Sprint = false;

            animator.SetBool("Aiming", true);
        }

        public override void Update()
        {
            if (!player.IsReloading)
            {
                animator.SetBool("Aiming", false);
                context.SwitchState(context.StandardState);
            }

            movement.MoveDirection = context.GetMovementDirection();
            movement.AnimateAimingMovement(inputs.move);
            movement.RotateByAim(player.AimPosition);
        }
    }
}
