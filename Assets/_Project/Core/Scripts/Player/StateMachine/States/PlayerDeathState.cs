using DemoProject.Input;
using DemoProject.Physics;
using UnityEngine;

namespace DemoProject.Player
{
    public class PlayerDeathState : PlayerState
    {
        private PlayerController m_Controller;
        private PlayerInputHandler m_InputHandler;
        private RagdollEnabler m_RagdollEnabler;
        
        public PlayerDeathState(PlayerContext ctx, bool needsExitTime = false, bool isGhostState = false) : base(ctx, needsExitTime, isGhostState)
        {
            m_Controller = Context.PlayerTransform.GetComponent<PlayerController>();
            m_InputHandler = Context.PlayerTransform.GetComponent<PlayerInputHandler>();
            m_RagdollEnabler = Context.PlayerTransform.GetComponent<RagdollEnabler>();
        }

        public override void OnEnter()
        {
            m_Controller.enabled = false;
            m_InputHandler.enabled = false;
            Context.Animator.enabled = false;
            m_RagdollEnabler.Enable();
        }
    }
}