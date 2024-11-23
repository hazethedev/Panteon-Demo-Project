using UnityEngine;

namespace DemoProject.Player
{
    public class JoystickRotationBehaviour : IPlayerRotationBehaviour
    {
        private readonly Transform m_PlayerTransform;
        private readonly PlayerController m_Controller;
        
        public JoystickRotationBehaviour(PlayerController controller)
        {
            m_Controller = controller;
            m_PlayerTransform = controller.transform;
        }
        
        void IPlayerRotationBehaviour.Rotate(in Vector3 movementInput, float deltaTime)
        {
            var heading = Mathf.Atan2(movementInput.x, movementInput.z) * Mathf.Rad2Deg;

            if (Mathf.Approximately(heading, 0)) return;
            var rotation = Quaternion.Euler(heading * Vector3.up);
            m_PlayerTransform.rotation = Quaternion.Slerp(m_PlayerTransform.rotation, rotation, m_Controller.RotationSpeed * deltaTime);
        }
    }
}