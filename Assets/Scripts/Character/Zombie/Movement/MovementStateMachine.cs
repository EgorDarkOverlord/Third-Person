using UnityEngine;

namespace Zombie.Movement
{
    public class MovementStateMachine : StateMachine<MovementStateMachine>
    {
        public ZombieController Zombie;

        public Transform Player;
        public Animator Animator;
        public ZombieMovment Movement;
        public Ragdoll Ragdoll;

        public IdleState IdleState { get; private set; }
        public FollowState FollowState { get; private set; }
        public DiedState DiedState { get; private set; }



        public MovementStateMachine(ZombieController zombie)
        {
            Zombie = zombie;

            Player = zombie.Player;
            Animator = zombie.Animator;
            Movement = zombie.Movement;
            Ragdoll = zombie.Ragdoll;

            IdleState = new IdleState(this);
            FollowState = new FollowState(this);
            DiedState = new DiedState(this);
            CurrentState = IdleState;
            CurrentState.Enter();
        }
    }
}
