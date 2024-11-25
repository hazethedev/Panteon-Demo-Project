using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DemoProject.Animations
{
    public abstract class ObstacleTweenAnimationBase<T> : MonoBehaviour
    {
        [SerializeField]
        protected Transform m_Transform;

#if UNITY_EDITOR
        [OnValueChanged(nameof(RestartAnimation))]
#endif
        [HideLabel, BoxGroup]
        public T AnimationData;

        protected Tween m_CurrentTween;

        protected abstract Tween CreateTween();

        protected virtual void OnEnable()
        {
            if (m_CurrentTween.IsActive())
            {
                m_CurrentTween.Play();
                return;
            }

            m_CurrentTween = CreateTween();
        }
        
        protected virtual void OnDisable()
        {
            if (m_CurrentTween.IsActive())
            {
                m_CurrentTween.Pause();
            }
        }
        
#if UNITY_EDITOR
        
        protected virtual void OnPreCreateTweenEditor() {}
        private void RestartAnimation()
        {
            if (!Application.isPlaying || !m_CurrentTween.IsActive()) return;
            if (!enabled) return;
            m_CurrentTween.Kill();
            OnPreCreateTweenEditor();
            m_CurrentTween = CreateTween();
        }
#endif
    }
}