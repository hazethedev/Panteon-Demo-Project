using DemoProject.Input;
using DemoProject.Misc;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DemoProject.Player
{
    public class PlayerController : MonoBehaviour, IMovementInputReceiver, ITargetLookAt
    {
        [SerializeField] private CharacterController m_CharacterController;
        [SerializeField] private MovementHandler m_MovementHandler;

        public float MovementSpeed;
        public float RotationSpeed;

        private Vector3 m_CurrentInput;
        private Vector3 m_CurrentVelocity;
        private int m_ModifierId;
        public Vector3 CurrentInput => m_CurrentInput;

        private JoystickRotationBehaviour m_JoystickRotation;
        private TargetLockRotationBehaviour m_TargetLockRotation;
        private IPlayerRotationBehaviour m_CurrentRotationBehaviour;

        public bool InDangerZone = false;
        [ShowInInspector]
        public bool IsGrounded => m_CharacterController.isGrounded;

        private void Awake()
        {
            m_JoystickRotation = new JoystickRotationBehaviour(this);
            m_TargetLockRotation = new TargetLockRotationBehaviour(this);
            m_CurrentRotationBehaviour = m_JoystickRotation;
        }

        private void Update()
        {
            m_CurrentVelocity = m_MovementHandler.GetVelocity();
            m_CurrentRotationBehaviour.Rotate(in m_CurrentInput, Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if (!m_CharacterController.isGrounded) m_CurrentVelocity += UnityEngine.Physics.gravity;
            m_CharacterController.Move(m_CurrentVelocity * Time.fixedDeltaTime);
        }

        public void SetInput(Vector3 input, InputEvent @event)
        {
            m_CurrentInput = input;
            
            switch (@event)
            {
                case InputEvent.Start:
                    m_ModifierId = m_MovementHandler.AddModifier(m_CurrentInput * MovementSpeed);
                    break;
                case InputEvent.Perform:
                    m_MovementHandler.ChangeModifier(m_ModifierId, m_CurrentInput * MovementSpeed);
                    break;
                case InputEvent.Cancel:
                    m_MovementHandler.RemoveModifier(m_ModifierId);
                        break;
                default:
                    break;
            }
        }
        
        public void LookAt(Transform target)
        {
            if (m_CurrentRotationBehaviour is TargetLockRotationBehaviour) return;
            m_TargetLockRotation.Target = target;
            m_CurrentRotationBehaviour = m_TargetLockRotation;
        }

        public void ReleaseTarget(Transform target)
        {
            m_TargetLockRotation.Target = null;
            m_CurrentRotationBehaviour = m_JoystickRotation;
        }
    }
}

























