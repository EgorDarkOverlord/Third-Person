using UnityEngine;

namespace Player.Movement
{
    public class StandardState : MovementState
    {
        public StandardState(MovementStateMachine context) : base(context)
        {
        }

        public override void Update()
        {
            if (player.IsAiming)
            {
                context.SwitchState(context.AimingState);
            }

            if(player.IsReloading)
            {
                context.SwitchState(context.ReloadState);
            }

            movement.MoveDirection = context.GetMovementDirection();
            movement.RotateByMoving();
            movement.Sprint = inputs.sprint;
            movement.Jump = inputs.jump;

            inputs.jump = false;
        }
    }
}