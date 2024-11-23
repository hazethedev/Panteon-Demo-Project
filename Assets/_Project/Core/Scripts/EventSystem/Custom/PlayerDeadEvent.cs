using DemoProject.EventSystem;
using UnityEngine;
using UnityEngine.Events;

namespace DemoProject.EventSystem
{
    [CreateAssetMenu(fileName = "New Player Dead Event", menuName = "EventSystem/Events/Player Dead Event")]
    public class PlayerDeadEvent : GameEventBase<Transform> {}
    
    [System.Serializable]
    public class UnityTransformEvent : UnityEvent<Transform> { }
}