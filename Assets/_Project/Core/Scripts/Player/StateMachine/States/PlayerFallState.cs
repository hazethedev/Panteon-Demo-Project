namespace DemoProject.Player
{
    public class PlayerFallState : PlayerState
    {
        public PlayerFallState(PlayerContext ctx, bool needsExitTime = false, bool isGhostState = false) : base(ctx, needsExitTime, isGhostState)
        {
        }
    }
}