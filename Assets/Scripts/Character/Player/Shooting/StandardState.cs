using System.Collections;
using UnityEngine;

namespace Player.Shooting
{
    public class StandardState : ShootingState
    {
        public StandardState(ShootingStateMachine context) : base(context)
        {
        }

        public override void Update()
        {
            if (shooting.WeaponEquipping)
                return;

            if (inputs.reload)
            {
                context.SwitchState(context.ReloadState);
                inputs.reload = false;
            }

            if (inputs.dropWeapon)
            {
                shooting.DropActiveWeapon();
                context.SwitchState(context.NoWeaponState);
                inputs.dropWeapon = false;
            }

            if (inputs.aim)
            {
                context.SwitchState(context.AimingState);
            }

            if (inputs.selectedWeaponNumber != 0)
            {
                shooting.StartCoroutine(SwitchWeapon(inputs.selectedWeaponNumber - 1));
                inputs.selectedWeaponNumber = 0;
            }

            if (inputs.hideOrShowWeapon)
            {
                shooting.StartCoroutine(HideActiveWeapon());
                inputs.hideOrShowWeapon = false;
            }
        }

        private IEnumerator HideActiveWeapon()
        {
            yield return shooting.StartCoroutine(shooting.HideActiveWeapon());
            context.SwitchState(context.NoWeaponState);
        }

        private IEnumerator SwitchWeapon(int selectedWeaponNumber)
        {
            yield return shooting.StartCoroutine(shooting.HideActiveWeapon());

            shooting.SelectWeapon(selectedWeaponNumber);
            if (shooting.CurrentWeapon != null)
            {
                yield return shooting.StartCoroutine(shooting.ShowActiveWeapon());
            }
            else
            {
                context.SwitchState(context.NoWeaponState);
            }
        }
    }
}