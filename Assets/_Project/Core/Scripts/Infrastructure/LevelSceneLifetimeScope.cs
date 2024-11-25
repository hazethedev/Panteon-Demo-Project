using CrashKonijn.Goap.Behaviours;
using DemoProject.EventSystem;
using DemoProject.LevelManagement;
using DemoProject.Player;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace DemoProject.Infrastructure
{
    public class LevelSceneLifetimeScope : LifetimeScope
    {
        [SerializeField] private CompetitorBase m_PlayerPrefab;
        [SerializeField] private CompetitorBase m_AIPrefab;
        [SerializeField] private PlayerDeadEvent m_PlayerDeadEventChannel;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(m_PlayerDeadEventChannel);
            builder.RegisterComponentInHierarchy<OnScreenStick>();
            builder.RegisterComponentInHierarchy<GoapRunnerBehaviour>().AsSelf();
            builder.RegisterComponentInHierarchy<CameraManager>().AsSelf();
            builder.RegisterFactory<CompetitorType, CompetitorBase>(container =>
            {
                return competitorType =>
                {
                    return competitorType switch
                    {
                        CompetitorType.Player => container.Instantiate(m_PlayerPrefab),
                        _ => container.Instantiate(m_AIPrefab),
                    };
                };
            }, Lifetime.Scoped);
        }
    }
}