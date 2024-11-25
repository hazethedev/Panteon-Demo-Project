using System;
using Cinemachine;
using DemoProject.Player;
using UnityEngine;

namespace DemoProject
{
    public class CameraManager : MonoBehaviour
    {
        public CinemachineBrain Brain;
        public LevelCamera[] Cameras;

        private const int k_DisabledPriority = 10;
        private const int k_EnabledPriority = 20;

        private LevelCamera m_Current;

        private void Awake()
        {
            SetCamera(CameraType.Gameplay);
        }

        public void SetCamera(CameraType type)
        {
            foreach (var levelCamera in Cameras)
            {
                if (levelCamera.Type == type)
                {
                    m_Current = levelCamera;
                    m_Current.VirtualCamera.Priority = k_EnabledPriority;
                    continue;
                }

                levelCamera.VirtualCamera.Priority = k_DisabledPriority;
            }
        }

        public ICinemachineCamera GetCamera(CameraType type)
        {
            foreach (var levelCamera in Cameras)
            {
                if (levelCamera.Type == type)
                {
                    return levelCamera.VirtualCamera;
                }
            }

            return null;
        }

        public void OnPlayerDead(Transform playerTransform)
        {
            SetCamera(CameraType.Death);
        }
    }

    public enum CameraType
    {
        Gameplay,
        Death,
        Finish
    }

    [Serializable]
    public class LevelCamera
    {
        public CameraType Type;
        public CinemachineVirtualCamera VirtualCamera;
    }
}