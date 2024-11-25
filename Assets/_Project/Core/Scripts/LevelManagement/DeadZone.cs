using DemoProject.LevelManagement.Events;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DemoProject.LevelManagement
{
    public class DeadZone : MonoBehaviour
    {
        [SerializeField, Required, InfoBox("Competitor Enter DeadZone Event Channel")]
        private CompetitorEvent m_EventChannel;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<CompetitorBase>(out var competitor))
            {
                m_EventChannel.Raise(competitor);
            }
        }
    }
}