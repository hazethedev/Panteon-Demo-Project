using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DemoProject.Animations
{
    public class PathAnimation : ObstacleTweenAnimationBase<PathAnimationData>
    {
        [SerializeField]
        private Vector3[] m_Waypoints;

        private void Awake()
        {
            m_Transform.localPosition = m_Waypoints[0];
        }
        
        protected override Tween CreateTween()
        {
            var pathTween = m_Transform.DOLocalPath(m_Waypoints, AnimationData.Duration, AnimationData.PathType)
                .SetSpeedBased(AnimationData.SpeedBased)
                .SetEase(AnimationData.Ease)
                .SetLoops(-1, LoopType.Yoyo);
            pathTween.fullPosition = UnityEngine.Random.Range(0f, AnimationData.Duration);
            return pathTween;
        }
    }

    [Serializable]
    public struct PathAnimationData
    {
        [MinValue(0.001f)]
        public float Duration;
        public Ease Ease;
        public PathType PathType;
        public bool SpeedBased;
    }
}