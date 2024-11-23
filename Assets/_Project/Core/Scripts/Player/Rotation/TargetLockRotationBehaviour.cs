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
        
        void IPlayerRotationBehaviour.Rotate(in Vector3 input, float deltaTime)
        {
            m_Controller.transform.LookAt(Target, Vector3.up);
        }
    }
}