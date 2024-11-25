using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes.Builders;
using CrashKonijn.Goap.Configs.Interfaces;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Resolver;
using DemoProject.AI.Actions;
using DemoProject.AI.Goals;
using DemoProject.AI.Sensors;
using DemoProject.AI.Targets;
using DemoProject.AI.WorldKeys;

namespace DemoProject.AI.Factories
{
    public class GoapSetConfigFactory : GoapSetFactoryBase
    {
        public override IGoapSetConfig Create()
        {
            var builder = new GoapSetBuilder("CompetitorSet");

            builder.AddGoal<ReachPlatformEndGoal>()
                .AddCondition<HasReachedPlatformEnd>(Comparison.SmallerThan, 1);

            builder.AddAction<ReachPlatformEndAction>()
                .AddEffect<HasReachedPlatformEnd>(EffectType.Decrease)
                .SetTarget<PlatformEndTarget>()
                .SetInRange(2f);

            builder.AddTargetSensor<PlatformEndSensor>()
                .SetTarget<PlatformEndTarget>();
            
            return builder.Build();
        }
    }
}