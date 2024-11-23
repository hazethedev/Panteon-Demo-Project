using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace DemoProject.Utils
{
    public class SelfCleanupAddressable : MonoBehaviour
    {
        private void OnDestroy()
        {
            Addressables.ReleaseInstance(gameObject);
        }
    }
}