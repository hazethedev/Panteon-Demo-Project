using System;
using DemoProject.AI;
using DemoProject.LevelManagement.Events;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DemoProject.LevelManagement
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField, Required, InfoBox("Obstacle-Competitor Event Channel")]
        private CompetitorEvent m_EventChannel;

        public Transform[] AvoidanceWaypoints;
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<CompetitorBase>(out var competitor))
            {
                m_EventChannel.Raise(competitor);
            }
        }
    }
}