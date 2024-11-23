using System;
using DemoProject.Player;
using UnityEngine;

namespace DemoProject.Physics
{
    public class CollisionHandler : MonoBehaviour
    {
        public LayerMask ObstacleLayerMask;
        public PlayerStateMachineBehaviour StateMachine;
        
        // pismanlik
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if ((ObstacleLayerMask.value & (1 << hit.gameObject.layer)) != 0)
            {
                StateMachine.TriggerDeath();
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if ((ObstacleLayerMask.value & (1 << other.gameObject.layer)) != 0)
            {
                StateMachine.TriggerDeath();
            }
        }
    }
}