using UnityEngine;

namespace Player.Movement
{
    public class AimingState : MovementState
    {
        public AimingState(MovementStateMachine context) : base(context)
        {
        }

        public override void Enter()
        {
            movement.Jump = false;
            movement.Sprint = false;
        }

        public override void Update()
        {
            if (!player.IsAiming)
            {
                context.SwitchState(context.StandardState);
            }

            movement.MoveDirection = context.GetMovementDirection();
            movement.AnimateAimingMovement(inputs.move);
            movement.RotateByAim(player.AimPosition);
        }
    }
}
