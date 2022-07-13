using UnityEngine;
using Cinemachine;
using StarterAssets;

namespace Player.Shooting
{ 
    public class ShootingStateMachine : StateMachine<ShootingStateMachine>
    {
        public PlayerController Player;

        public StarterAssetsInputs Inputs;
        public Camera MainCamera;
        public Animator WeaponAnimator;
        public CinemachineImpulseSource CameraShake;
        public CinemachineVirtualCamera AimVirtualCamera;
        public GameObject PlayerAim;

        public PlayerCamera PlayerCamera;
        public WeaponInventory WeaponInventory;
        public CharacterShooting Shooting;

        private LayerMask _aimColliderLayerMask;

        public NoWeaponState NoWeaponState { get; private set; }
        public StandardState StandardState { get; private set; }
        public AimingState AimingState { get; private set; }
        public ReloadState ReloadState { get; private set; }



        public ShootingStateMachine(PlayerController player)
        {
            Player = player;

            Inputs = player.Inputs;
            MainCamera = player.MainCamera;
            WeaponAnimator = player.WeaponAnimator;
            CameraShake = player.CameraShake;
            AimVirtualCamera = player.AimVirtualCamera;
            PlayerAim = player.PlayerAim;

            PlayerCamera = player.PlayerCamera;
            WeaponInventory = player.WeaponInventory;
            Shooting = player.Shooting;

            _aimColliderLayerMask = player.AimColliderMask;
            
            NoWeaponState = new NoWeaponState(this);
            StandardState = new StandardState(this);
            AimingState = new AimingState(this);
            ReloadState = new ReloadState(this);
            CurrentState = NoWeaponState;
        }

        public Vector3 FindMouseWorldPosition()
        {
            Vector3 mouseWorldPosition = Vector3.zero;
            Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
            Ray ray = MainCamera.ScreenPointToRay(screenCenter);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, _aimColliderLayerMask))
            {
                mouseWorldPosition = raycastHit.point;
            }

            return mouseWorldPosition;
        }
    }
}
