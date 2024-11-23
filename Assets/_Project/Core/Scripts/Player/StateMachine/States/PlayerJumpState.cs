namespace DemoProject.Player
{
    public class PlayerJumpState : PlayerState
    {
        public PlayerJumpState(PlayerContext ctx, bool needsExitTime = false, bool isGhostState = false) : base(ctx, needsExitTime, isGhostState)
        {
        }
    }
}