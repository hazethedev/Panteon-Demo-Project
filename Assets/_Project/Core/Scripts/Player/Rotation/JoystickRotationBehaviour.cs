using UnityEngine;

namespace DemoProject.Player
{
    public class JoystickRotationBehaviour : IPlayerRotationBehaviour
    {
        private readonly PlayerController m_Controller;
        
        public JoystickRotationBehaviour(PlayerController controller)
        {
            m_Controller = controller;
        }
        
        void IPlayerRotationBehaviour.Rotate(in Vector3 currentInput, ref Quaternion currentRotation, float deltaTime)
        {
            // var currentInput = m_Controller.CurrentInput;
            var heading = Mathf.Atan2(currentInput.x, currentInput.z) * Mathf.Rad2Deg;

            if (Mathf.Approximately(heading, 0)) return;
            var rotation = Quaternion.Euler(heading * Vector3.up);
            currentRotation = Quaternion.Slerp(currentRotation, rotation, m_Controller.RotationSpeed * deltaTime);
        }
    }
}