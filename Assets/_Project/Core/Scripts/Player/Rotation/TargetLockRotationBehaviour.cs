using UnityEngine;

namespace DemoProject.Player
{
    public class TargetLockRotationBehaviour : IPlayerRotationBehaviour
    {
        private readonly PlayerController m_Controller;
        public Transform Target;
        
        public TargetLockRotationBehaviour(PlayerController controller)
        {
            m_Controller = controller;
        }
        
        void IPlayerRotationBehaviour.Rotate(in Vector3 currentInput, ref Quaternion currentRotation, float deltaTime)
        {
            var directionToTarget = Target.position - m_Controller.transform.position;
            if (!(directionToTarget.sqrMagnitude > 0f)) return;
            
            var targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);
            currentRotation = Quaternion.Slerp(currentRotation, targetRotation, m_Controller.RotationSpeed * deltaTime);
        }
    }
}