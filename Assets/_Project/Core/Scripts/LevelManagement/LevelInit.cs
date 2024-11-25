using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace DemoProject.LevelManagement
{
    public class LevelInit : MonoBehaviour
    {
        public LevelManager LevelManager;

        private Func<CompetitorType, CompetitorBase> m_CompetitorFactory;
        private CameraManager m_CameraManager;

        private async void Start()
        {
            await UniTask.WaitUntil(() => m_CompetitorFactory != null);
            var playerCompetitor = m_CompetitorFactory.Invoke(CompetitorType.Player);
            HandleCameras(playerCompetitor);

            const int playerClaimIndex = 5;
            LevelManager.RegisterCompetitor(playerCompetitor, playerClaimIndex);
            
            for (var i = 0; i < 11; i++)
            {
                if (i == playerClaimIndex) continue;
                var opponent = m_CompetitorFactory.Invoke(CompetitorType.AI);
                opponent.gameObject.SetActive(true);
                LevelManager.RegisterCompetitor(opponent, i);
            }
            
            LevelManager.GetReady();
        }

        private void HandleCameras(CompetitorBase playerCompetitor)
        {
            var gameplayCam = m_CameraManager.GetCamera(CameraType.Gameplay);
            var deathCam = m_CameraManager.GetCamera(CameraType.Death);
            gameplayCam.Follow = playerCompetitor.transform;
            gameplayCam.LookAt = playerCompetitor.transform;
            deathCam.Follow = playerCompetitor.transform;
            deathCam.LookAt = playerCompetitor.transform;
        }

        #region Dependency Injection

        [Inject]
        private void Construct(Func<CompetitorType, CompetitorBase> competitorFactory, CameraManager cameraManager)
        {
            m_CompetitorFactory = competitorFactory;
            m_CameraManager = cameraManager;
        }

        #endregion
    }
}