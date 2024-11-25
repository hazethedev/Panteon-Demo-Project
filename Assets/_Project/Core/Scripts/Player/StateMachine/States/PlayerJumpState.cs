using UnityEngine;
using UnityHFSM;

namespace DemoProject.Player
{
    public class PlayerJumpState : PlayerState
    {
        private PlayerController m_Controller;
        private Timer m_Timer;
        
        public PlayerJumpState(PlayerContext ctx, bool needsExitTime = false, bool isGhostState = false) : base(ctx, needsExitTime, isGhostState)
        {
            m_Controller = ctx.PlayerTransform.GetComponent<PlayerController>();
            m_Timer = new Timer();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            m_Timer.Reset();
            
            var currentInput = m_Controller.CurrentInput.normalized;

            var localInput = m_Controller.transform.InverseTransformDirection(new Vector3(currentInput.x, 0, currentInput.z));
            
            Context.Animator.SetInteger(Context.IsJumpingParamHash, localInput.z < 0 ? 1 : 2);
        }

        public override void OnLogic()
        {
            base.OnLogic();
            if (m_Timer.Elapsed >= .5f)
            {
                fsm.StateCanExit();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            Context.Animator.SetInteger(Context.IsJumpingParamHash, 0);
        }
    }
}