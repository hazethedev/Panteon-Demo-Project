using DemoProject.Player;
using UnityEngine;

namespace DemoProject.LevelManagement
{
    public class FinishLine : MonoBehaviour
    {
        [SerializeField] private Transform m_PlayerStandPoint;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<CompetitorBase>(out var competitor))
            {
                if (competitor is PlayerCompetitor playerCompetitor)
                {
                    // win
                    playerCompetitor.BeginFinishSequence(m_PlayerStandPoint.position);
                }
                else
                {
                    // lose
                }
            }
        }
    }
}