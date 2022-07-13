using UnityEngine;
using Cinemachine;
using StarterAssets;

namespace Player.Shooting
{
    public abstract class ShootingState : State<ShootingStateMachine>
    {
        protected PlayerController player;

        protected StarterAssetsInputs inputs;
        protected Camera mainCamera;
        protected Animator weaponAnimator;
        protected CinemachineImpulseSource cameraShake;
        protected CinemachineVirtualCamera aimVirtualCamera;
        protected GameObject playerAim;

        protected PlayerCamera playerCamera;
        protected WeaponInventory weaponInventory;
        protected CharacterShooting shooting;



        public ShootingState(ShootingStateMachine context) : base(context)
        {
            player = context.Player;        
            
            inputs = context.Inputs;
            mainCamera = context.MainCamera;
            weaponAnimator = context.WeaponAnimator;
            cameraShake = context.CameraShake;
            aimVirtualCamera = context.AimVirtualCamera;
            playerAim = context.PlayerAim;

            playerCamera = context.PlayerCamera;
            weaponInventory = context.WeaponInventory;
            shooting = context.Shooting;
        }
    }
}
