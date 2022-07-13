using UnityEngine;

namespace Zombie.Fighting
{
    public class ActiveState : FightingState
    {
        private int _fightIndex;

        public ActiveState(FightingStateMachine context) : base(context)
        {
        }

        public override void Enter()
        {
            fighting.StartFight();
        }

        public override void Update()
        {
            var direction = player.position - zombie.transform.position;
            direction.y = 0;          
            zombie.transform.forward = direction;
            
            if ((zombie.transform.position - player.position).sqrMagnitude > Mathf.Pow(zombie.FightDistance, 2))
            {
                context.SwitchState(context.IdleState);
            }

            if (fighting.IsHitting)
            {
                return;
            }

            fighting.Hit(_fightIndex++);
            if (_fightIndex > 3)
            {
                _fightIndex = 0;
            }
        }

        public override void Exit()
        {
            fighting.StopFight();
        }
    }
}
