using System;
using UnityEngine;
using Cinemachine;
using StarterAssets;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public CharacterController Controller;
        public Animator Animator;
        public Animator WeaponAnimator;
        public CinemachineImpulseSource CameraShake;
        public StarterAssetsInputs Inputs;
        public Camera MainCamera;
        public CinemachineVirtualCamera AimVirtualCamera;
        public LayerMask AimColliderMask;
        public GameObject PlayerAim;

        public CharacterMovement Movement;
        public PlayerShooting Shooting;
        public PlayerCamera PlayerCamera;
        public WeaponInventory WeaponInventory;
        public Ragdoll Ragdoll;
        public PlayerHealth Health;

        public Movement.MovementStateMachine MovementStateMachine;
        public Shooting.ShootingStateMachine ShootingStateMachine;

        public Vector3 AimPosition;
        public bool IsAiming;
        public bool IsReloading;
        
        private int _score;
        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                OnScoreChanged?.Invoke(value);
            }
        }

        public event Action<int> OnScoreChanged;



        private void Awake()
        {
            Controller = GetComponent<CharacterController>();
            Animator = GetComponent<Animator>();
            WeaponAnimator = transform.Find("Weapon Rig").GetComponent<Animator>();
            CameraShake = GetComponent<CinemachineImpulseSource>();
            Inputs = GetComponent<StarterAssetsInputs>();
            MainCamera = Camera.main;
            //AimVirtualCamera = GameObject.Find("PlayerAimCamera").GetComponent<CinemachineVirtualCamera>();
            PlayerAim = GameObject.Find("PlayerAim");

            Movement = GetComponent<CharacterMovement>();
            Shooting = GetComponent<PlayerShooting>();
            PlayerCamera = GetComponent<PlayerCamera>();
            WeaponInventory = GetComponent<WeaponInventory>();
            Ragdoll = GetComponent<Ragdoll>();
            Health = GetComponent<PlayerHealth>();

            Hitbox.AttachHitboxesToRagdoll(Ragdoll);
            Hitbox.RecieveHealthFromObjectToHitboxes(gameObject);

            MovementStateMachine = new Movement.MovementStateMachine(this);
            ShootingStateMachine = new Shooting.ShootingStateMachine(this);
        }

        private void OnEnable()
        {
            Health.OnDied += OnDied;
        }

        private void OnDisable()
        {
            Health.OnDied += OnDied;
        }

        private void Update()
        {
            MovementStateMachine.Update();
            ShootingStateMachine.Update();
        }

        private void OnTriggerEnter(Collider collision)
        {
            var weaponPickup = collision.transform.parent.GetComponent<WeaponPickup>();
            if (weaponPickup != null)
            {
                if (weaponPickup.Ammo.Count != 0)
                {
                    weaponPickup.PickupAmmo(WeaponInventory);
                }
            }
        }

        private void OnTriggerStay(Collider collision)
        {
            if (Inputs.pickupWeapon)
            {
                var weaponPickup = collision.transform.parent.GetComponent<WeaponPickup>();
                if (weaponPickup != null)
                {
                    weaponPickup.Pickup(WeaponInventory);
                }

                Inputs.pickupWeapon = false;
            }
        }

        private void OnDied()
        {
            MovementStateMachine.SwitchState(MovementStateMachine.DiedState);
        }           
    }
}
