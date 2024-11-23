
using UnityEngine;
using UnityEngine.Events;

namespace DemoProject.EventSystem
{
    public abstract class GameEventListenerBase<TEventArgs, TEvent, TUnityEventResponse> : MonoBehaviour, IGameEventListener<TEventArgs>
        where TEvent              : GameEventBase<TEventArgs>
        where TUnityEventResponse : UnityEvent<TEventArgs>
    {
        [field: SerializeField] public TEvent GameEvent { get; set; }
        [SerializeField] private TUnityEventResponse m_UnityEventResponse;

        private void OnEnable()
        {
            if (!GameEvent) return;

            GameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            if (!GameEvent) return;

            GameEvent.UnregisterListener(this);
        }

        public void OnEventRaised(TEventArgs item) => m_UnityEventResponse?.Invoke(item);
    }
}
