using System.Collections.Generic;
using UnityEngine;

namespace DemoProject.EventSystem
{
    public abstract class GameEventBase<T> : ScriptableObject
    {
        private readonly List<IGameEventListener<T>> m_GameEventListeners = new List<IGameEventListener<T>>();

        public void Raise(T item)
        {
            for (var i = m_GameEventListeners.Count - 1; i >= 0; i--)
            {
                var listener = m_GameEventListeners[i];
                listener.OnEventRaised(item);
            }
        }

        public void RegisterListener(IGameEventListener<T> listener)
        {
            if (!m_GameEventListeners.Contains(listener))
            {
                m_GameEventListeners.Add(listener);
            }
        }

        public void UnregisterListener(IGameEventListener<T> listener)
        {
            if (m_GameEventListeners.Contains(listener))
            {
                m_GameEventListeners.Remove(listener);
            }
        }
    }
}
