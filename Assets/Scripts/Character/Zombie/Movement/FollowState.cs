using UnityEngine;

namespace Zombie.Movement
{
    public class FollowState : MovementState
    {
        public FollowState(MovementStateMachine context) : base(context)
        {
        }

        public override void Enter()
        {
            movement.Speed = movement.RunSpeed;
        }

        public override void Update()
        {
            if ((zombie.transform.position - player.position).sqrMagnitude > Mathf.Pow(zombie.FollowDistance, 2))
            {
                context.SwitchState(context.IdleState);
            }            
        }
    }
}
