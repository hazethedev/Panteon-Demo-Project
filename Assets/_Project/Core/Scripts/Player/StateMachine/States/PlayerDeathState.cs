using DemoProject.Input;
using DemoProject.Physics;

namespace DemoProject.Player
{
    public class PlayerDeathState : PlayerState
    {
        private PlayerController m_Controller;
        private PlayerInputHandler m_InputHandler;
        private RagdollController m_RagdollController;
        
        public PlayerDeathState(PlayerContext ctx, bool needsExitTime = false, bool isGhostState = false) : base(ctx, needsExitTime, isGhostState)
        {
            m_Controller = Context.PlayerTransform.GetComponent<PlayerController>();
            m_InputHandler = Context.PlayerTransform.GetComponent<PlayerInputHandler>();
            m_RagdollController = Context.PlayerTransform.GetComponent<RagdollController>();
        }

        public override void OnEnter()
        {
            m_Controller.enabled = false;
            m_InputHandler.enabled = false;
            Context.Animator.enabled = false;
            m_RagdollController.Enable();
            Context.CameraManager.SetDeathCamera();

            Context.PlayerDeadEventChannel.Raise(Context.PlayerTransform);
        }
    }
}