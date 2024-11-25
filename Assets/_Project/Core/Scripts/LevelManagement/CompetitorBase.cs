using System;
using UnityEngine;

namespace DemoProject.LevelManagement
{
    public abstract class CompetitorBase : MonoBehaviour
    {
        public Platform CurrentPlatform;
        [NonSerialized] public bool IsDead;

        [NonSerialized] public int ClaimIndex = -1;
        
        public abstract void RespawnAt(Vector3 position, Quaternion rotation);
        
        public abstract void Push(Vector3 pushDirection);
        public abstract void StartRacing();

        public virtual void OnPlatformChange(Platform current, Platform previous) {}

        public abstract void Die();

        public void ChangePlatform(Platform to)
        {
            var previous = CurrentPlatform;
            CurrentPlatform = to;
            OnPlatformChange(CurrentPlatform, previous);
        }
    }

    public enum CompetitorType
    {
        Player,
        AI
    }
}