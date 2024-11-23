using System.Collections.Generic;
using DemoProject.Player;
using UnityEngine;

namespace DemoProject.Physics
{
    public class VelocityProvider : MonoBehaviour
    {
        public Vector3 Velocity;
        private Dictionary<MovementHandler, int> m_ModifierDictionary;

        private void Awake()
        {
            m_ModifierDictionary = new Dictionary<MovementHandler, int>(capacity: 4);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<MovementHandler>(out var movementHandler))
            {
                if (m_ModifierDictionary.TryAdd(movementHandler, -1))
                {
                    var modifierId = movementHandler.AddModifier(Velocity);
                    m_ModifierDictionary[movementHandler] = modifierId;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<MovementHandler>(out var movementHandler))
            {
                if (m_ModifierDictionary.Remove(movementHandler, out var modifierId))
                {
                    movementHandler.RemoveModifier(modifierId);
                }
            }
        }
    }
}