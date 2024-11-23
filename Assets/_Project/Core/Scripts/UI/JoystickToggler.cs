using DemoProject.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using VContainer;

namespace DemoProject.UI
{
    public class JoystickToggler : MonoBehaviour
    {
        [SerializeField] private Image m_JoystickImage;

        private GameInputs m_GameInputs;
        private bool m_WaitingForInjection = false;

        private void OnEnable()
        {
            if (m_GameInputs == null)
            {
                m_WaitingForInjection = true;
                enabled = false;
                return;
            }

            m_GameInputs.Enable();
            m_GameInputs.Player.Move.started += OnMoveStart;
            m_GameInputs.Player.Move.canceled += OnMoveCancel;
        }
        
        private void OnDisable()
        {
            if (m_GameInputs == null) return;
            m_GameInputs.Player.Move.started -= OnMoveStart;
            m_GameInputs.Player.Move.canceled -= OnMoveCancel;
        }

        private void OnMoveCancel(InputAction.CallbackContext _)
        {
            m_JoystickImage.enabled = false;
        }

        private void OnMoveStart(InputAction.CallbackContext _)
        {
            m_JoystickImage.enabled = true;
        }

        #region Dependency Injection

        [Inject]
        private void Construct(GameInputs gameInputs)
        {
            m_GameInputs = gameInputs;
            
            if (!enabled && m_WaitingForInjection)
            {
                enabled = true;
                m_WaitingForInjection = false;
            }
        }

        #endregion
    }
}