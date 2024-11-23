using System;
using DemoProject.Player;
using DG.Tweening;
using UnityEngine;

namespace DemoProject.Physics
{
    public class ForceApplier : MonoBehaviour
    {
        public Vector3 Force;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out MovementHandler handler))
            {
                var modifierId = -1;
                DOVirtual.Float(0f, .25f, .25f, _ =>
                    {
                        // handler.ChangeModifier(new Vector3(0f, 10f, 500f));
                    })
                    .OnStart(() => modifierId = handler.AddModifier(Force))
                    .OnComplete(() => handler.RemoveModifier(modifierId));

            }
        }
    }
}