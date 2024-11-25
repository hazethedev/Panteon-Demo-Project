using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;
using VContainer;

namespace DemoProject.Input
{
    [RequireComponent(typeof(IMovementInputReceiver))]
    public class PlayerInputHandler : MonoBehaviour
    {
        private IMovementInputReceiver m_MovementInputReceiver;
        private GameInputs m_GameInputs;
        private OnScreenStick m_ScreenStick;
        private bool m_WaitingForInjection = false;

        private void Awake()
        {
            m_MovementInputReceiver = GetComponent<IMovementInputReceiver>();
        }

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
            m_GameInputs.Player.Move.performed += OnMovePerform;
            m_GameInputs.Player.Move.canceled += OnMoveCancel;
            m_ScreenStick.enabled = true;
        }

        private void OnDisable()
        {
            if (m_GameInputs == null) return;
            m_GameInputs.Player.Move.started -= OnMoveStart;
            m_GameInputs.Player.Move.performed -= OnMovePerform;
            m_GameInputs.Player.Move.canceled -= OnMoveCancel;
            m_GameInputs.Disable();
            if (m_ScreenStick != null) m_ScreenStick.enabled = false;
            m_MovementInputReceiver.SetInput(Vector3.zero, InputEvent.Cancel);
        }

        private void OnMoveCancel(InputAction.CallbackContext ctx)
        {
            m_MovementInputReceiver.SetInput(Vector3.zero, InputEvent.Cancel);
        }

        private void OnMovePerform(InputAction.CallbackContext ctx)
        {
            var inputRaw = ctx.ReadValue<Vector2>();
            var input = new Vector3(inputRaw.x, 0f, inputRaw.y);
            m_MovementInputReceiver.SetInput(input, InputEvent.Perform);
        }

        private void OnMoveStart(InputAction.CallbackContext ctx)
        {
            var inputRaw = ctx.ReadValue<Vector2>();
            var input = new Vector3(inputRaw.x, 0f, inputRaw.y);
            m_MovementInputReceiver.SetInput(input, InputEvent.Start);
        }

        #region Dependency Injection

        [Inject]
        private void Construct(GameInputs gameInputs, OnScreenStick screenStick)
        {
            m_GameInputs = gameInputs;
            m_ScreenStick = screenStick;

            if (!enabled && m_WaitingForInjection)
            {
                enabled = true;
                m_WaitingForInjection = false;
            }
        }

        #endregion
    }
}