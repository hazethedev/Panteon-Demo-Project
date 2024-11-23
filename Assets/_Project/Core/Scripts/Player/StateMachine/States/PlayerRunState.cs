namespace DemoProject.Player
{
    public class PlayerRunState : PlayerState
    {
        public PlayerRunState(PlayerContext ctx, bool needsExitTime = false, bool isGhostState = false) : base(ctx, needsExitTime, isGhostState)
        {
        }
    }
}