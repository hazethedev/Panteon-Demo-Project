using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DemoProject.Input;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace DemoProject.Infrastructure
{
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private AssetLabelReference m_BootAssetLabelReference;

        private Transform m_InfrastructureParent;

        protected override async void Awake()
        {
            await EnqueueInfrastructure();
            base.Awake();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<GameInputs>(Lifetime.Singleton).AsSelf();
            var children = m_InfrastructureParent.GetComponentsInChildren<MonoBehaviour>(includeInactive: true);
            foreach (var child in children)
            {
                builder.RegisterComponentInHierarchy(child.GetType()).AsImplementedInterfaces().AsSelf();
            }
        }

        private async UniTask EnqueueInfrastructure()
        {
            const string parentName = "Infrastructure";
            var parentGameObject = new GameObject(parentName);
            DontDestroyOnLoad(parentGameObject);
            m_InfrastructureParent = parentGameObject.transform;

            var opHandle = Addressables.LoadAssetsAsync<GameObject>(m_BootAssetLabelReference.RuntimeKey, addressable =>
            {
                var instance = Instantiate(addressable, Vector3.zero, Quaternion.identity);
                instance.transform.SetParent(m_InfrastructureParent);
            });

            await opHandle.Task;
            Addressables.Release(opHandle);
        }
    }
    
    
}
