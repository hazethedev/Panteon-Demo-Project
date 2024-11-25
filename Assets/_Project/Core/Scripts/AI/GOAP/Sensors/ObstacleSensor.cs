using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using UnityEngine;

namespace DemoProject.AI.Sensors
{
    public class ObstacleSensor : LocalTargetSensorBase
    {
        private Collider[] m_Colliders;
        private LayerMask m_ObstacleLayerMask;
        
        public override void Created()
        {
            m_Colliders = new Collider[1];
            m_ObstacleLayerMask = LayerMask.NameToLayer("Obstacle");
        }

        public override void Update()
        {
            
        }

        public override ITarget Sense(IMonoAgent agent, IComponentReference references)
        {
            const float sensorRadius = 10f;
            if (UnityEngine.Physics.OverlapSphereNonAlloc(agent.transform.position, sensorRadius, m_Colliders,
                    m_ObstacleLayerMask) > 0)
            {
                return new TransformTarget(m_Colliders[0].transform);
            }

            return null;
        }
    }
}