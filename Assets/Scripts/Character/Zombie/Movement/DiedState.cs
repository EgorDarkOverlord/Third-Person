using UnityEngine;

namespace Zombie.Movement
{
    public class DiedState : MovementState
    {
        public DiedState(MovementStateMachine context) : base(context)
        {
        }

        public override void Enter()
        {
            ragdoll.Activate();
            movement.Speed = 0;
        }
    }
}
