using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Interfaces;
using DemoProject.AI.Data;

namespace DemoProject.AI.Actions
{
    public class ReachPlatformEndAction : ActionBase<CommonData>
    {
        public override void Created()
        {
        }

        public override void Start(IMonoAgent agent, CommonData data)
        {
        }

        public override ActionRunState Perform(IMonoAgent agent, CommonData data, ActionContext context)
        {
            return ActionRunState.Stop;
        }

        public override void End(IMonoAgent agent, CommonData data)
        {
            
        }
    }
}