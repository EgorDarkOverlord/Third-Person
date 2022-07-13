using UnityEngine;

namespace Zombie.Movement
{
    public class IdleState : MovementState
    {
        public IdleState(MovementStateMachine context) : base(context)
        {
        }

        public override void Enter()
        {
            movement.Speed = 0;
        }

        public override void Update()
        {
            if ((zombie.transform.position - player.position).sqrMagnitude < Mathf.Pow(zombie.FollowDistance, 2))
            {
                context.SwitchState(context.FollowState);
            }
        }
    }
}
