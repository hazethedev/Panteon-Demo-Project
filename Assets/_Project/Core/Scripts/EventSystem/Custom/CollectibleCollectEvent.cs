using DemoProject.Collectibles;
using UnityEngine;
using UnityEngine.Events;

namespace DemoProject.EventSystem
{
    [CreateAssetMenu(fileName = "New Collectible Collect Event", menuName = "EventSystem/Events/Collectible Collect Event")]
    public class CollectibleCollectEvent : GameEventBase<Collectible> {}
    
    [System.Serializable]
    public class UnityCollectibleEvent : UnityEvent<Collectible> { }
}