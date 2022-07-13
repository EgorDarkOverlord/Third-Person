using UnityEngine;
using Zombie;
using Zombie.Movement;
using Zombie.Fighting;

public class ZombieController : MonoBehaviour
{
    public float FollowDistance;
    public float FightDistance;

    public Transform Player;
    public Animator Animator;

    public ZombieMovment Movement;
    public CharacterFighting Fighting;
    public Ragdoll Ragdoll;
    public CharacterHealth Health;

    public MovementStateMachine MovementStateMachine;
    public FightingStateMachine FightingStateMachine;



    private void Awake()
    {
        Player = GameObject.FindWithTag("Player").transform;

        Animator = GetComponent<Animator>();
        Movement = GetComponent<ZombieMovment>();
        Fighting = GetComponent<CharacterFighting>();
        Ragdoll = GetComponent<Ragdoll>();
        Health = GetComponent<CharacterHealth>();

        Movement.PlayerTransform = Player;

        Hitbox.AttachHitboxesToRagdoll(Ragdoll);
        Hitbox.RecieveHealthFromObjectToHitboxes(gameObject);     
    }

    private void OnEnable()
    {
        Health.OnDied += OnDied;
    }

    private void OnDisable()
    {
        Health.OnDied -= OnDied;
    }

    private void Start()
    {
        MovementStateMachine = new MovementStateMachine(this);
        FightingStateMachine = new FightingStateMachine(this);
    }

    private void Update()
    {
        MovementStateMachine.Update();
        FightingStateMachine.Update();
    }



    private void OnDied()
    {
        MovementStateMachine.SwitchState(MovementStateMachine.DiedState);
        FightingStateMachine.SwitchState(FightingStateMachine.DiedState);

        GetComponent<CapsuleCollider>().enabled = false;

        Player.GetComponent<Player.PlayerController>().Score += 1;
    }
}