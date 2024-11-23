using UnityEngine;
using UnityHFSM;

namespace DemoProject.Player
{
    public class GroundedState : StateMachine<PlayerStateId, PlayerStateTriggerEvent>
    {
        private PlayerController m_Controller;
        private Animator m_Animator;
        private int m_IsGroundedParamHash;
        private int m_MovementBlendXParamHash;
        private int m_MovementBlendYParamHash;

        public GroundedState(PlayerContext ctx)
        {
            m_Controller = ctx.PlayerTransform.GetComponent<PlayerController>();
            m_Animator = ctx.PlayerTransform.GetComponentInChildren<Animator>();
            m_IsGroundedParamHash = ctx.IsGroundedParamHash;
            m_MovementBlendXParamHash = ctx.MovementBlendXParamHash;
            m_MovementBlendYParamHash = ctx.MovementBlendYParamHash;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            m_Animator.SetBool(m_IsGroundedParamHash, true);
        }

        public override void OnLogic()
        {
            base.OnLogic();
            HandleAnimation();

            if (!m_Controller.IsGrounded)
            {
                if (m_Controller.InDangerZone)
                {
                    ((StateMachine<PlayerStateId, PlayerStateTriggerEvent>)ParentFsm).Trigger(PlayerStateTriggerEvent.Dead);
                }
            }
        }

        private void HandleAnimation()
        {
            var currentInput = m_Controller.CurrentInput.normalized;

            var localInput = m_Controller.transform.InverseTransformDirection(new Vector3(currentInput.x, 0, currentInput.z));

            m_Animator.SetFloat(m_MovementBlendXParamHash, localInput.x);
            m_Animator.SetFloat(m_MovementBlendYParamHash, localInput.z);
        }

        public override void OnExit()
        {
            base.OnEnter();
            m_Animator.SetBool(m_IsGroundedParamHash, false);
        }
    }
}