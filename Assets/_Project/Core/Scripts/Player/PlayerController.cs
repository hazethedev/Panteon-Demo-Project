using System;
using DemoProject.Input;
using DemoProject.Misc;
using KinematicCharacterController;
using UnityEngine;

namespace DemoProject.Player
{
    public class PlayerController : MonoBehaviour, IMovementInputReceiver, ITargetLookAt, ICharacterController
    {
        [SerializeField] private MovementHandler m_MovementHandler;
        [SerializeField] private PlayerStateMachineBehaviour m_PlayerStateMachine;
        
        public float MovementSpeed;
        public float RotationSpeed;

        private Vector3 m_CurrentInput;
        public Vector3 CurrentInput => m_CurrentInput;

        private JoystickRotationBehaviour m_JoystickRotation;
        private TargetLockRotationBehaviour m_TargetLockRotation;
        private IPlayerRotationBehaviour m_CurrentRotationBehaviour;

        private KinematicCharacterMotor m_Motor;
        private Vector3 m_InternalVelocityAddForMotor;

        private void Awake()
        {
            m_JoystickRotation = new JoystickRotationBehaviour(this);
            m_TargetLockRotation = new TargetLockRotationBehaviour(this);
            m_CurrentRotationBehaviour = m_JoystickRotation;
            m_Motor = GetComponent<KinematicCharacterMotor>();
            m_Motor.CharacterController = this;
        }

        public void SetInput(Vector3 input, InputEvent @event) => m_CurrentInput = input;
        
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

        public void AddVelocity(Vector3 velocity) => m_InternalVelocityAddForMotor += velocity;

        void ICharacterController.UpdateRotation(ref Quaternion currentRotation, float deltaTime) =>
            m_CurrentRotationBehaviour.Rotate(in m_CurrentInput, ref currentRotation, deltaTime);

        void ICharacterController.UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
        {
            if (!enabled) return;
            const float airAccelerationSpeed = 1f;
            const float drag = .1f;
            var gravity = new Vector3(0f, -30f, 0f);
            var targetMovementVelocity = Vector3.zero;
            if (m_Motor.GroundingStatus.IsStableOnGround)
            {
                // Reorient source velocity on current ground slope (this is because we don't want our smoothing to cause any velocity losses in slope changes)
                currentVelocity = m_Motor.GetDirectionTangentToSurface(currentVelocity, m_Motor.GroundingStatus.GroundNormal) * currentVelocity.magnitude;

                // Calculate target velocity
                var inputRight = Vector3.Cross(m_CurrentInput, m_Motor.CharacterUp);
                var reorientedInput = Vector3.Cross(m_Motor.GroundingStatus.GroundNormal, inputRight).normalized * m_CurrentInput.magnitude;
                currentVelocity = reorientedInput * MovementSpeed;
                currentVelocity += m_MovementHandler.GetVelocity();

                // Smooth movement Velocity
                // currentVelocity = Vector3.Lerp(currentVelocity, targetMovementVelocity, 1 - Mathf.Exp(-15f * deltaTime));
            }
            else
            {
                // Add move input
                if (m_CurrentInput.sqrMagnitude > 0f)
                {
                    targetMovementVelocity = m_CurrentInput * MovementSpeed;

                    // Prevent climbing on un-stable slopes with air movement
                    if (m_Motor.GroundingStatus.FoundAnyGround)
                    {
                        var perpenticularObstructionNormal = Vector3.Cross(Vector3.Cross(m_Motor.CharacterUp, m_Motor.GroundingStatus.GroundNormal), m_Motor.CharacterUp).normalized;
                        targetMovementVelocity = Vector3.ProjectOnPlane(targetMovementVelocity, perpenticularObstructionNormal);
                    }

                    var velocityDiff = Vector3.ProjectOnPlane(targetMovementVelocity - currentVelocity, gravity);
                    currentVelocity += velocityDiff * (airAccelerationSpeed * deltaTime);
                }

                // Gravity
                currentVelocity += gravity * deltaTime;

                // Drag
                currentVelocity *= (1f / (1f + (drag * deltaTime)));
            }
            
            // Take into account additive velocity
            if (m_InternalVelocityAddForMotor.sqrMagnitude > 0f)
            {
                currentVelocity += m_InternalVelocityAddForMotor;
                m_InternalVelocityAddForMotor = Vector3.zero;
            }
        }
        
        void ICharacterController.PostGroundingUpdate(float deltaTime)
        {
            // Handle landing and leaving ground
            if (m_Motor.GroundingStatus.IsStableOnGround && !m_Motor.LastGroundingStatus.IsStableOnGround)
            {
                // landed
            }
            else if (!m_Motor.GroundingStatus.IsStableOnGround && m_Motor.LastGroundingStatus.IsStableOnGround)
            {
                // left ground
            }
        }
    }
}

























