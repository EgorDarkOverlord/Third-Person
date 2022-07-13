using UnityEngine;

namespace Zombie.Fighting
{
    public abstract class FightingState : State<FightingStateMachine>
    {
        protected ZombieController zombie;

        protected Transform player;
        protected Animator animator;
        protected CharacterFighting fighting;
        protected Ragdoll ragdoll;



        public FightingState(FightingStateMachine context) : base(context)
        {
            zombie = context.Zombie;
            player = context.Player;
            animator = context.Animator;
            fighting = context.Fighting;
            ragdoll = context.Ragdoll;
        }
    }
}
