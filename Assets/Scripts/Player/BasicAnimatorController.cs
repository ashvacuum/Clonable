using System;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class BasicAnimatorController : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private Animator _animator;
        private static readonly int VelocityX = Animator.StringToHash("Velocity X");
        private static readonly int VelocityZ = Animator.StringToHash("Velocity Z");
        private static readonly int Moving = Animator.StringToHash("Moving");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private PointAndClickController _clickController;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            Movement();
        }

        public virtual void Movement()
        {
            var currentVelocity = GetCurrentVelocity();
            _animator.SetBool(Moving, currentVelocity.magnitude > .3f);
            _animator.SetFloat(VelocityX, GetCurrentVelocity().x);
            _animator.SetFloat(VelocityZ, GetCurrentVelocity().z);
        }

        private Vector3 GetCurrentVelocity()
        {
            return _agent == null ? Vector3.zero : _agent.velocity.normalized;
        }
        
        
        
        public virtual void TriggerAttack()
        {
            _animator.SetTrigger(Attack);
        }

    }
}
