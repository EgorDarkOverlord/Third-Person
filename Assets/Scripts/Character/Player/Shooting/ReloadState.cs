using System.Collections;
using UnityEngine;

namespace Player.Shooting
{
    public class ReloadState : ShootingState
    {
        public ReloadState(ShootingStateMachine context) : base(context)
        {
        }

        public override void Enter()
        {
            player.IsReloading = true;
            shooting.StartCoroutine(Reload());
        }

        public override void Update()
        {
            Vector3 mouseWorldPosition = context.FindMouseWorldPosition();
            playerAim.transform.position = mouseWorldPosition;
            player.AimPosition = mouseWorldPosition;
        }

        public override void Exit()
        {
            player.IsReloading = false;
        }

        private IEnumerator Reload()
        {
            yield return shooting.StartCoroutine(shooting.Reload());
            context.SwitchState(context.StandardState);
        }
    }
}
