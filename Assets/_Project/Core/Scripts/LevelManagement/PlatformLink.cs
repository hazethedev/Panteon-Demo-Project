using System;
using UnityEngine;

namespace DemoProject.LevelManagement
{
    public class PlatformLink : MonoBehaviour
    {
        public Platform Next;
        public Platform Previous;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<CompetitorBase>(out var competitor))
            {
                competitor.ChangePlatform(competitor.CurrentPlatform == Previous ? Next : Previous);
            }
        }
    }
}