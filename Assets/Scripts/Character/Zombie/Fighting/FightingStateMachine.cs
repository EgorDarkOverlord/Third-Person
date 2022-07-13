using UnityEngine;

namespace Zombie.Fighting
{
    public class FightingStateMachine : StateMachine<FightingStateMachine>
    {
        public ZombieController Zombie;

        public Transform Player;
        public Animator Animator;
        public CharacterFighting Fighting;
        public Ragdoll Ragdoll;

        public IdleState IdleState { get; private set; }
        public ActiveState ActiveState { get; private set; }
        public DiedState DiedState { get; private set; }



        public FightingStateMachine(ZombieController zombie)
        {
            Zombie = zombie;

            Player = zombie.Player;
            Animator = zombie.Animator;
            Fighting = zombie.Fighting;
            Ragdoll = zombie.Ragdoll;

            IdleState = new IdleState(this);
            ActiveState = new ActiveState(this);
            DiedState = new DiedState(this);
            CurrentState = IdleState;
            CurrentState.Enter();
        }
    }
}
