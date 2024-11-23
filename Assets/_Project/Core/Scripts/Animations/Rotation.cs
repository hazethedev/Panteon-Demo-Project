using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DemoProject.Animations
{
    public class Rotation : ObstacleTweenAnimationBase<RotationAnimationData>
    {
        protected override Tween CreateTween()
        {
            var amount = Vector3.zero;
            amount[(int)AnimationData.Axis] = AnimationData.Reverse ? -360f : 360f;
            
            return m_Transform.DOLocalRotate(amount, AnimationData.Duration, RotateMode.LocalAxisAdd)
                .SetSpeedBased(AnimationData.SpeedBased)
                .SetLoops(-1)
                .SetEase(AnimationData.Ease);
        }
    }

    [Serializable]
    public struct RotationAnimationData
    {
        [MinValue(0.001f)]
        public float Duration;
        public Ease Ease;
        public Axis Axis;
        public bool Reverse;
        public bool SpeedBased;
    }

    public enum Axis
    {
        X, Y, Z
    }
}