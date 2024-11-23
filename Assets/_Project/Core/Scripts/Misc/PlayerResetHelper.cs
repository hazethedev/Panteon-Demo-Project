using DemoProject.Input;
using DemoProject.Physics;
using DemoProject.Player;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace DemoProject.Misc
{
    public class PlayerResetHelper : MonoBehaviour, IResetHelper
    {
        public PlayerController PlayerController;
        public PlayerInputHandler InputHandler;
        public PlayerStateMachineBehaviour StateMachine;
        public MovementHandler MovementHandler;
        public RagdollController RagdollController;
        public Animator PlayerAnimator;

        public CameraManager CameraManager;
        
        [Button]
        public void ResetAll()
        {
            gameObject.SetActive(false);
            
            CameraManager.SetGameplayCamera();
            RagdollController.Disable();
            RagdollController.BackToOriginalPosition();
            MovementHandler.ClearModifiers();
            StateMachine.ResetMachine();
            PlayerAnimator.enabled = true;
            PlayerController.enabled = true;
            InputHandler.enabled = true;
            PlayerController.transform.position = Vector3.zero;
            PlayerController.transform.rotation = Quaternion.identity;
            PlayerController.InDangerZone = false;
            PlayerController.ReleaseTarget(null);
            
            gameObject.SetActive(true);
        }

        #region Dependency Injection

        [Inject]
        private void Construct(CameraManager cameraManager)
        {
            CameraManager = cameraManager;
        }

        #endregion
    }
}