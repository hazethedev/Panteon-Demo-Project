using System;
using UnityEngine;

namespace DemoProject.AI.Temp
{
    public class RandomMoveProvider : MonoBehaviour
    {
        public Transform[] PointBuffer;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<AICompetitor>(out var aiCompetitor))
            {
                var competitorPosition = aiCompetitor.transform.position;

                var closestPoint = GetClosestPoint(competitorPosition);
                aiCompetitor.MoveSafePoint(closestPoint);
            }
        }
        
        private Transform GetClosestPoint(Vector3 position)
        {
            Transform closestPoint = null;
            var closestDistanceSqr = 500f; // maxlike

            foreach (var point in PointBuffer)
            {
                var distanceSqr = (point.position - position).sqrMagnitude;
                if (distanceSqr < closestDistanceSqr)
                {
                    closestDistanceSqr = distanceSqr;
                    closestPoint = point;
                }
            }

            return closestPoint;
        }
    }
}