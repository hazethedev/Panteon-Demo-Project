using CrashKonijn.Goap.Behaviours;
using DemoProject.AI.Behaviours;
using DemoProject.AI.Goals;
using DemoProject.LevelManagement;
using DemoProject.Physics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace DemoProject.AI
{
    public class AICompetitor : CompetitorBase
    {
        public CompetitorBrain Brain;
        public NavMeshAgent NavMeshAgent;
        public AgentMoveBehaviour AgentMoveBehaviour;
        public AgentBehaviour AgentBehaviour;
        public Animator Animator;
        public RagdollEnabler RagdollEnabler;
        
        public override void RespawnAt(Vector3 position, Quaternion rotation)
        {
            gameObject.SetActive(false);

            RagdollEnabler.Disable();
            RagdollEnabler.BackToOriginalPosition();
            
            // AgentBehaviour.enabled = true;
            // NavMeshAgent.enabled = true;
            // AgentMoveBehaviour.enabled = true;
            
            Animator.enabled = true;
            transform.SetPositionAndRotation(position, rotation);
            
            gameObject.SetActive(true);
            // Brain.SetInitialGoal();
        }

        public override void Push(Vector3 pushDirection)
        {
            return;
        }

        public override void StartRacing()
        {
            AgentBehaviour.enabled = true;
            NavMeshAgent.enabled = true;
            AgentMoveBehaviour.enabled = true;
            Brain.SetInitialGoal();
        }

        public override void Die()
        {
            Animator.enabled = false;
            NavMeshAgent.enabled = false;
            AgentBehaviour.enabled = false;
            AgentMoveBehaviour.enabled = false;
            RagdollEnabler.Enable();
        }

        public override void OnPlatformChange(Platform current, Platform previous)
        {
            Animator.SetTrigger("_jumping");
            Brain.SetInitialGoal();
        }

        public void MoveSafePoint(Transform point)
        {
            AgentMoveBehaviour.MoveSafePoint(point);
        }
    }
}