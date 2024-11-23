using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DemoProject.Animations
{
    public class PushAnimation : ObstacleTweenAnimationBase<PushAnimationData>
    {
        protected override Tween CreateTween()
        {
            var sequence = DOTween.Sequence();
            
            var waitDuration = UnityEngine.Random.Range(AnimationData.MinInterval, AnimationData.MaxInterval);

            var pushTween = m_Transform.DOLocalMove(AnimationData.TargetLocalPosition, AnimationData.PushDuration)
                .SetEase(AnimationData.Ease);
            var pullTween = m_Transform.DOLocalMove(AnimationData.InitialLocalPosition, AnimationData.PullDuration)
                .SetEase(AnimationData.Ease);

            sequence.Append(pushTween)
                .Append(pullTween)
                .SetLoops(-1)
                .AppendInterval(waitDuration);

            return sequence;
        }
#if UNITY_EDITOR
        protected override void OnPreCreateTweenEditor()
        {
            m_Transform.localPosition = AnimationData.InitialLocalPosition;
        }
#endif
    }
    
    [Serializable]
    public struct PushAnimationData
    {
        public float PushDuration;
        public float PullDuration;
        public float MaxInterval;
        public float MinInterval;
        public Ease Ease;
        public Vector3 InitialLocalPosition;
        public Vector3 TargetLocalPosition;
    }
}