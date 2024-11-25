using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using UnityEngine;

namespace DemoProject.AI.Sensors
{
    public class PlatformEndSensor : LocalTargetSensorBase
    {
        public override void Created() {}

        public override void Update() {}

        public override ITarget Sense(IMonoAgent agent, IComponentReference references)
        {
            var competitor = references.GetCachedComponent<AICompetitor>();
            var currentPlatform = competitor.CurrentPlatform;
            var endPoint = currentPlatform.EndPoint;

            // Debug.Log("PlatformEndSensor" + endPoint.gameObject);
            return new PositionTarget(endPoint.position);
        }
    }
}