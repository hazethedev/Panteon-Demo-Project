using System;
using DemoProject.EventSystem;
using DemoProject.Infrastructure;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityHFSM;
using VContainer;
using VContainer.Unity;

namespace DemoProject.Player
{
    public class PlayerStateMachineBehaviour : MonoBehaviour
    {
        private StateMachine<PlayerStateId, PlayerStateTriggerEvent> m_StateMachine;
        
        [SerializeField, HideLabel, BoxGroup]
        private PlayerContext m_PlayerContext;

        private void Awake()
        {
            var playerController = m_PlayerContext.PlayerTransform.GetComponent<PlayerController>();
            
            m_StateMachine = new StateMachine<PlayerStateId, PlayerStateTriggerEvent>();
            m_PlayerContext.Init();

            var groundState = new GroundedState(m_PlayerContext);
            var airState = new AirState(m_PlayerContext);

            var idleState = new PlayerIdleState(m_PlayerContext);
            var runState = new PlayerRunState(m_PlayerContext);

            var jumpState = new PlayerJumpState(m_PlayerContext);
            var fallState = new PlayerFallState(m_PlayerContext);

            var deathState = new PlayerDeathState(m_PlayerContext);

            m_StateMachine.AddState(PlayerStateId.Ground, groundState);
            m_StateMachine.AddState(PlayerStateId.Air, airState);
            m_StateMachine.AddState(PlayerStateId.Death, deathState);
            m_StateMachine.SetStartState(PlayerStateId.Ground);
            m_StateMachine.AddTriggerTransition(PlayerStateTriggerEvent.Throw, PlayerStateId.Ground, PlayerStateId.Air);
            m_StateMachine.AddTriggerTransitionFromAny(PlayerStateTriggerEvent.Dead, PlayerStateId.Death,
                forceInstantly: true);

            groundState.AddState(PlayerStateId.Idle, idleState);
            groundState.AddState(PlayerStateId.Run, runState);
            groundState.SetStartState(PlayerStateId.Idle);
            groundState.AddTwoWayTransition(PlayerStateId.Idle, PlayerStateId.Run,
                t => playerController.CurrentInput != Vector3.zero, forceInstantly: true);

            airState.AddState(PlayerStateId.Jump, jumpState);
            airState.AddState(PlayerStateId.Fall, fallState);
            airState.SetStartState(PlayerStateId.Jump);

            m_StateMachine.Init();
        }

        public void ResetMachine() => m_StateMachine.Init();

        private void Update()
        {
            m_StateMachine.OnLogic();
        }

        public void TriggerDeath()
        {
            m_StateMachine.Trigger(PlayerStateTriggerEvent.Dead);
        }

        #region Dependency Injection

        [Inject]
        private void Construct(CameraManager cameraManager, PlayerDeadEvent playerDeadEventChannel)
        {
            m_PlayerContext.CameraManager = cameraManager;
            m_PlayerContext.PlayerDeadEventChannel = playerDeadEventChannel;
        }

        #endregion
    }
}