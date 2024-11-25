using UnityEngine;

namespace DemoProject.Player
{
    public class PlayerIdleState : PlayerState
    {
        // private BoxCollider m_PlayerCollider;
        
        public PlayerIdleState(PlayerContext ctx, bool needsExitTime = false, bool isGhostState = false) : base(ctx, needsExitTime, isGhostState)
        {
            // m_PlayerCollider = ctx.PlayerTransform.GetComponent<BoxCollider>();
        }

        // public override void OnEnter()
        // {
        //     base.OnEnter();
        //     m_PlayerCollider.isTrigger = false;
        // }
        //
        // public override void OnExit()
        // {
        //     base.OnExit();
        //     m_PlayerCollider.isTrigger = true;
        // }
    }
}