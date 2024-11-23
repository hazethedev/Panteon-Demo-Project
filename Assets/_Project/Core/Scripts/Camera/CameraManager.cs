using System;
using Cinemachine;
using DemoProject.Player;
using UnityEngine;

namespace DemoProject
{
    public class CameraManager : MonoBehaviour
    {
        public CinemachineVirtualCamera GameplayCamera;
        public CinemachineVirtualCamera DeathCamera;

        private void Awake()
        {
            GameplayCamera.Priority = 20;
            DeathCamera.Priority = 10;
            DeathCamera.enabled = false;
        }

        public void SetDeathCamera()
        {
            GameplayCamera.Priority = 10;
            DeathCamera.Priority = 20;
            DeathCamera.enabled = true;
            GameplayCamera.enabled = false;
        }
        
        public void SetGameplayCamera()
        {
            DeathCamera.Priority = 10;
            GameplayCamera.Priority = 20;
            GameplayCamera.enabled = true;
            DeathCamera.enabled = false;
        }
    }
}