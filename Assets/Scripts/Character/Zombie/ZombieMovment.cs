using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Zombie
{
    public class ZombieMovment : MonoBehaviour
    {
        public float WalkSpeed;
        public float RunSpeed;

        private float _speed;
        public float Speed
        {
            get => _speed;
            set { _speed = value; _navMeshAgent.speed = value; }
        }

        public Transform PlayerTransform;

        private NavMeshAgent _navMeshAgent;
        private Animator _animator;



        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _navMeshAgent.SetDestination(PlayerTransform.position);
            _animator.SetFloat("Speed", _navMeshAgent.velocity.magnitude);
        }
    }
}
