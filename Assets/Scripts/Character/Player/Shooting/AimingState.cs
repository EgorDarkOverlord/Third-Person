using UnityEngine;

namespace Player.Shooting
{
    public class AimingState : ShootingState
    {
        public AimingState(ShootingStateMachine context) : base(context)
        {
        }

        public override void Enter()
        {
            player.IsAiming = true;
            aimVirtualCamera.gameObject.SetActive(true);
            playerCamera.Sensivity = playerCamera.AimSensivity;
            shooting.AnimateAiming();
        }

        public override void Update()
        {
            if (!inputs.aim)
            {
                context.SwitchState(context.StandardState);
            }

            if (inputs.reload)
            {
                context.SwitchState(context.ReloadState);
                inputs.reload = false;
            }

            Vector3 mouseWorldPosition = context.FindMouseWorldPosition();
            playerAim.transform.position = mouseWorldPosition;
            player.AimPosition = mouseWorldPosition;

            if (inputs.shoot)
            {
                if (weaponInventory.CurrentWeapon.CanShoot())
                {
                    Vector3 aimDirection = (mouseWorldPosition - weaponInventory.CurrentWeapon.SpawnBulletTransform.position).normalized;
                    shooting.Shoot(aimDirection);
                    //cameraShake.GenerateImpulse();
                    //weaponAnimator.SetTrigger("Shoot"); 

                    if (shooting.CurrentWeapon.Ammo == 0)
                    {
                        context.SwitchState(context.ReloadState);
                    }
                }
            }
        }

        public override void Exit()
        {
            player.IsAiming = false;
            aimVirtualCamera.gameObject.SetActive(false);
            playerCamera.Sensivity = playerCamera.IdleSensivity;
            shooting.AnimateNonAiming();
        }
    }
}
