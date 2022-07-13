using UnityEngine;

namespace Player.Movement
{
    public class DiedState : MovementState
    {
        public DiedState(MovementStateMachine context) : base(context)
        {
        }

        public override void Enter()
        {
            movement.Jump = false;
            movement.Sprint = false;
            movement.MoveDirection = Vector2.zero;

            player.Controller.enabled = false;            
            ragdoll.Activate();
        }
    }
}
