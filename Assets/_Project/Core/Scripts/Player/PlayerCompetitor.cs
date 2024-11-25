using System.Collections;
using DemoProject.EventSystem;
using DemoProject.Input;
using DemoProject.LevelManagement;
using DemoProject.Physics;
using KinematicCharacterController;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;

namespace DemoProject.Player
{
    public class PlayerCompetitor : CompetitorBase
    {
        [SerializeField] private PlayerController m_PlayerController;
        [SerializeField] private PlayerInputHandler m_InputHandler;
        [SerializeField] private PlayerStateMachineBehaviour m_PlayerStateMachineBehaviour;
        [SerializeField] private MovementHandler m_MovementHandler;
        [FormerlySerializedAs("m_RagdollController")] [SerializeField] private RagdollEnabler ragdollEnabler;
        [SerializeField] private Animator m_PlayerAnimator;
        [SerializeField] private KinematicCharacterMotor m_CharacterMotor;
        
        private CameraManager m_CameraManager;
        private PlayerDeadEvent m_PlayerDeadEventChannel;
        
        public override void Die()
        {
            m_PlayerStateMachineBehaviour.Trigger(PlayerStateTriggerEvent.Dead);
            m_PlayerDeadEventChannel.Raise(transform);
        }

        public override void StartRacing()
        {
            m_InputHandler.enabled = true;
            m_PlayerController.enabled = true;
        }

        public override void Push(Vector3 pushDirection)
        {
            m_CharacterMotor.ForceUnground(0.1f);
            m_PlayerController.AddVelocity(pushDirection);
        }

        public override void OnPlatformChange(Platform current, Platform previous)
        {
            m_PlayerStateMachineBehaviour.Trigger(PlayerStateTriggerEvent.Jump);
        }

        public override void RespawnAt(Vector3 position, Quaternion rotation)
        {
            gameObject.SetActive(false);
            
            m_CameraManager.SetCamera(CameraType.Gameplay);
            ragdollEnabler.Disable();
            ragdollEnabler.BackToOriginalPosition();
            m_MovementHandler.ClearModifiers();
            m_PlayerStateMachineBehaviour.ResetMachine();
            m_PlayerAnimator.enabled = true;
            // m_PlayerController.enabled = true;
            // m_InputHandler.enabled = true;
            m_CharacterMotor.ApplyState(new KinematicCharacterMotorState { Position = position, Rotation = rotation });
            m_PlayerController.ReleaseTarget(null);
            
            gameObject.SetActive(true);
        }

        public void BeginFinishSequence(Vector3 targetPosition)
        {
            StartCoroutine(MoveToPositionCoroutine(targetPosition));
        }
        
        private IEnumerator MoveToPositionCoroutine(Vector3 targetPosition)
        {
            m_CameraManager.SetCamera(CameraType.Finish);
            m_InputHandler.enabled = false;
            var currentPosition = m_PlayerController.transform.position;
            const float reachThreshold = .1f;

            while (Vector3.Distance(currentPosition, targetPosition) > reachThreshold)
            {
                var direction = (targetPosition - currentPosition).normalized;
                direction.y = 0f;

                m_PlayerController.SetInput(direction, InputEvent.Perform);

                currentPosition = m_PlayerController.transform.position;

                yield return null;
            }

            m_PlayerController.SetInput(Vector3.zero, InputEvent.Cancel);
            m_CharacterMotor.ApplyState(new KinematicCharacterMotorState { Position = targetPosition, Rotation = Quaternion.identity });
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        #region Dependency Injection

        [Inject]
        private void Construct(CameraManager cameraManager, PlayerDeadEvent playerDeadEvent)
        {
            m_CameraManager = cameraManager;
            m_PlayerDeadEventChannel = playerDeadEvent;
        }

        #endregion
    }
}