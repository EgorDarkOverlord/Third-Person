using System.Collections;
using UnityEngine;

namespace Player.Shooting
{
    public class NoWeaponState : ShootingState
    {
        public NoWeaponState(ShootingStateMachine context) : base(context)
        {
        }

        public override void Update()
        {
            if (shooting.WeaponEquipping)
                return;

            if (inputs.selectedWeaponNumber != 0)
            {
                shooting.SelectWeapon(inputs.selectedWeaponNumber - 1);

                inputs.selectedWeaponNumber = 0;
            }

            if (inputs.hideOrShowWeapon)
            {
                if (shooting.CurrentWeapon != null)
                {
                    shooting.StartCoroutine(ShowActiveWeapon());
                }

                inputs.hideOrShowWeapon = false;
            }
        }

        private IEnumerator ShowActiveWeapon()
        {
            yield return shooting.StartCoroutine(shooting.ShowActiveWeapon());
            context.SwitchState(context.StandardState);
        }
    }
}
