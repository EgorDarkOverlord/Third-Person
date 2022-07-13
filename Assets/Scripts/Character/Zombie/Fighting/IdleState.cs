using UnityEngine;

namespace Zombie.Fighting
{
    public class IdleState : FightingState
    {
        public IdleState(FightingStateMachine context) : base(context)
        {
        }

        public override void Update()
        {
            if ((zombie.transform.position - player.position).sqrMagnitude < Mathf.Pow(zombie.FightDistance, 2))
            {
                context.SwitchState(context.ActiveState);
            }
        }
    }
}
