using System;
using DemoProject.Misc;
using DemoProject.Player;
using UnityEngine;
using VContainer;

namespace DemoProject.LevelManagement
{
    [DefaultExecutionOrder(-5)]
    public class LevelManager : Singleton<LevelManager>
    {
        public Platform InitialPlatform;
        
        public Transform[] SpawnPoints;
        
        [NonSerialized]
        public CompetitorBase[] Competitors;

        private CameraManager m_CameraManager;

        protected override void Awake()
        {
            base.Awake();
            Competitors = new CompetitorBase[SpawnPoints.Length];
        }

        public void GetReady()
        {
            foreach (var competitor in Competitors)
            {
                if (competitor == null) continue;
                var claimIndex = competitor.ClaimIndex;
                var spawnPoint = SpawnPoints[claimIndex];
                competitor.CurrentPlatform = InitialPlatform;
                competitor.RespawnAt(spawnPoint.position, spawnPoint.rotation);
            }
        }

        public void StartRacing()
        {
            foreach (var competitor in Competitors)
            {
                if (competitor == null) continue;
                competitor.StartRacing();
            }
        }

        public void RegisterCompetitor(CompetitorBase competitor, int claimIndex = -1)
        {
            Competitors[claimIndex] = competitor;
            competitor.ClaimIndex = claimIndex;
        }

        public void KillCompetitor(CompetitorBase competitor)
        {
            if (competitor.IsDead) return;
            
            competitor.IsDead = true;
            competitor.Die();
            
            CoroutineRunner.Instance.Run(competitor.GetInstanceID(), 1.5f, () =>
            {
                var spawnPoint = SpawnPoints[competitor.ClaimIndex];
                competitor.RespawnAt(spawnPoint.position, spawnPoint.rotation);
                competitor.IsDead = false;
                
                CoroutineRunner.Instance.Run(competitor.transform.GetInstanceID(), .1f, competitor.StartRacing);
            });
        }

        [Inject]
        private void Construct(CameraManager cameraManager)
        {
            m_CameraManager = cameraManager;
        }
    }
}