using System;
using DemoProject.Player;
using UnityEngine;

namespace DemoProject.Platform
{
    public class DangerZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerController>(out var controller))
            {
                controller.InDangerZone = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<PlayerController>(out var controller))
            {
                controller.InDangerZone = false;
            }
        }
    }
}