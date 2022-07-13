using UnityEngine;
using StarterAssets;

namespace Player.Movement
{
    public class MovementStateMachine : StateMachine<MovementStateMachine>
    {
        public PlayerController Player;

        public StarterAssetsInputs Inputs;
        public Camera MainCamera;
        public Animator Animator;
        public CharacterMovement Movement;
        public Ragdoll Ragdoll;

        public StandardState StandardState { get; private set; }
        public AimingState AimingState { get; private set; }
        public ReloadState ReloadState { get; private set; }
        public DiedState DiedState { get; private set; }



        public MovementStateMachine(PlayerController player)
        {
            Player = player;

            Inputs = player.Inputs;
            MainCamera = player.MainCamera;
            Animator = player.Animator;
            Movement = player.Movement;
            Ragdoll = player.Ragdoll;

            StandardState = new StandardState(this);
            AimingState = new AimingState(this);
            ReloadState = new ReloadState(this);
            DiedState = new DiedState(this);
            CurrentState = StandardState;
        }

        public Vector2 GetMovementDirection()
        {
            if (Inputs.move != Vector2.zero)
            {
                Vector3 inputDirection = new Vector3(Inputs.move.x, 0.0f, Inputs.move.y).normalized;
                float targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + MainCamera.transform.eulerAngles.y;
                Vector3 targetDirection = (Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward).normalized;
                return new Vector2(targetDirection.x, targetDirection.z);
            }
            else
            {
                return Vector2.zero;
            }
        }
    }
}
