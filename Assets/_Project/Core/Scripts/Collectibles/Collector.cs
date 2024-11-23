using DemoProject.EventSystem;
using UnityEngine;

namespace DemoProject.Collectibles
{
    public class Collector : MonoBehaviour
    {
        [SerializeField] private CollectibleCollectEvent m_CollectEventChannel;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Collectible>(out var collectible))
            {
                collectible.OnCollected(this);
                m_CollectEventChannel.Raise(collectible);
            }
        }
    }
}