using System;
using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace DemoProject.AI.Behaviours
{
    public class AgentMoveBehaviour : MonoBehaviour
    {
        public NavMeshAgent NavMeshAgent;
        public Animator Animator;
        public AgentBehaviour AgentBehaviour;
        public CompetitorBrain Brain;

        private ITarget m_CurrentTarget;

        private void OnEnable()
        {
            AgentBehaviour.Events.OnTargetChanged += OnTargetChanged;
            AgentBehaviour.Events.OnTargetOutOfRange += OnTargetOutOfRange;
        }

        private void OnDisable()
        {
            AgentBehaviour.Events.OnTargetChanged -= OnTargetChanged;
            AgentBehaviour.Events.OnTargetOutOfRange -= OnTargetOutOfRange;
        }

        private void OnTargetOutOfRange(ITarget target)
        {
            m_CurrentTarget = target;
        }

        private void OnTargetChanged(ITarget target, bool inRange)
        {
            m_CurrentTarget = target;
            NavMeshAgent.SetDestination(m_CurrentTarget.Position);
        }
        
        private void Update()
        {
            if (movingSafePoint && !NavMeshAgent.pathPending && NavMeshAgent.remainingDistance <= NavMeshAgent.stoppingDistance)
            {
                if (!NavMeshAgent.hasPath || NavMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    movingSafePoint = false;
                    NavMeshAgent.SetDestination(m_CurrentTarget.Position);
                    Brain.SetInitialGoal();
                }
            }
            
            var velocity = NavMeshAgent.velocity;

            var movementX = velocity.x;
            var movementY = velocity.z;

            Animator.SetFloat("_movementBlendX", movementX);
            Animator.SetFloat("_movementBlendY", movementY);
        }

        private bool movingSafePoint = false;
        public void MoveSafePoint(Transform point)
        {
            movingSafePoint = true;
            NavMeshAgent.SetDestination(point.position);
            
        }
    }
}