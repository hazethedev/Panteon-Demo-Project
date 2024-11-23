using DemoProject.EventSystem;
using DemoProject.Player;
using VContainer;
using VContainer.Unity;

namespace DemoProject.Infrastructure
{
    public class LevelSceneLifetimeScope : LifetimeScope
    {
        public PlayerDeadEvent PlayerDeadEventChannel;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(PlayerDeadEventChannel).AsSelf();
            builder.RegisterComponentInHierarchy<CameraManager>().AsSelf();
            builder.Register<JoystickRotationBehaviour>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
            builder.Register<TargetLockRotationBehaviour>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
        }
    }
}