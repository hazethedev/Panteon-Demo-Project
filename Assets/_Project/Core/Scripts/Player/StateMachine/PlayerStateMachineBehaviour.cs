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

        [SerializeField, HideLabel, BoxGroup] private PlayerContext m_PlayerContext;

        private void Awake()
        {
            var playerController = m_PlayerContext.PlayerTransform.GetComponent<PlayerController>();

            m_StateMachine = new StateMachine<PlayerStateId, PlayerStateTriggerEvent>();
            m_PlayerContext.Init();

            var groundState = new GroundedState(m_PlayerContext);

            var idleState = new PlayerIdleState(m_PlayerContext);
            var runState = new PlayerRunState(m_PlayerContext);

            var jumpState = new PlayerJumpState(m_PlayerContext, needsExitTime: true);
            var deathState = new PlayerDeathState(m_PlayerContext);

            m_StateMachine.AddState(PlayerStateId.Ground, groundState);
            m_StateMachine.AddState(PlayerStateId.Death, deathState);
            m_StateMachine.AddState(PlayerStateId.Jump, jumpState);
            m_StateMachine.SetStartState(PlayerStateId.Ground);
            m_StateMachine.AddTriggerTransitionFromAny(PlayerStateTriggerEvent.Dead, PlayerStateId.Death,
                forceInstantly: true);
            m_StateMachine.AddTriggerTransitionFromAny(PlayerStateTriggerEvent.Jump, PlayerStateId.Jump,
                forceInstantly: true, afterTransition: (t) => m_StateMachine.Trigger(PlayerStateTriggerEvent.Ground));
            m_StateMachine.AddTriggerTransitionFromAny(PlayerStateTriggerEvent.Ground, PlayerStateId.Ground);

            groundState.AddState(PlayerStateId.Idle, idleState);
            groundState.AddState(PlayerStateId.Run, runState);
            groundState.SetStartState(PlayerStateId.Idle);
            groundState.AddTwoWayTransition(PlayerStateId.Idle, PlayerStateId.Run,
                t => playerController.CurrentInput != Vector3.zero, forceInstantly: true);

            m_StateMachine.Init();
        }

        public void ResetMachine() => m_StateMachine.Init();
        public void Trigger(PlayerStateTriggerEvent triggerEvent) => m_StateMachine.Trigger(triggerEvent);

        private void Update()
        {
            m_StateMachine.OnLogic();
        }
    }
}