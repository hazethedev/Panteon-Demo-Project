using System;
using System.Collections;
using DemoProject.EventSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DemoProject.Player
{
    [Serializable]
    public class PlayerContext
    {
        [Header("References")]
        public Animator Animator;
        public Transform PlayerTransform;
        // public PlayerController Controller;
        public CameraManager CameraManager;

        [Header("Animator")]
        [ValueDropdown(nameof(GetAnimatorParamsDropdown))]
        public string MovementBlendXParamName;
        [HideInInspector] public int MovementBlendXParamHash;
        
        [ValueDropdown(nameof(GetAnimatorParamsDropdown))]
        public string MovementBlendYParamName;
        [HideInInspector] public int MovementBlendYParamHash;

        [ValueDropdown(nameof(GetAnimatorParamsDropdown))]
        public string IsGroundedParamName;
        [HideInInspector] public int IsGroundedParamHash;

        [NonSerialized] 
        public PlayerDeadEvent PlayerDeadEventChannel;

        public void Init()
        {
            MovementBlendXParamHash = Animator.StringToHash(MovementBlendXParamName);
            MovementBlendYParamHash = Animator.StringToHash(MovementBlendYParamName);
            IsGroundedParamHash = Animator.StringToHash(IsGroundedParamName);
        }

#if UNITY_EDITOR
        private IEnumerable GetAnimatorParamsDropdown()
        {
            if (Animator == null) return null;
            var parameters = Animator.parameters;
            return Array.ConvertAll(parameters, (param) => param.name);
        }
#endif
    }
}