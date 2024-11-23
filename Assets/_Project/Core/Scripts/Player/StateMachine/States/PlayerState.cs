using UnityHFSM;

namespace DemoProject.Player
{
    public class PlayerState : StateBase<PlayerStateId>
    {
        public PlayerContext Context;
        
        public PlayerState(PlayerContext ctx, bool needsExitTime = false, bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            Context = ctx;
        }
    }
}