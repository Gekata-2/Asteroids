using System;
using UnityEngine.InputSystem;

namespace Player
{
    public interface IInput
    {
        event Action PlayerPerformedMovingForward;
        event Action PlayerCanceledMovingForward;

        float PlayerRotation();

        void Enable();
        void Disable();
    }


    public class InputHandler : IInput
    {
        public event Action PlayerPerformedMovingForward;
        public event Action PlayerCanceledMovingForward;

        private readonly PlayerInputActionMap _playerInput;

        public InputHandler(PlayerInputActionMap playerInput)
        {
            _playerInput = playerInput;
        }

        public void Enable()
        {
            _playerInput.Player.Enable();
            _playerInput.Player.MoveForward.performed += MoveForward_OnStarted;
            _playerInput.Player.MoveForward.canceled += MoveForward_OnCanceled;
        }

        private void MoveForward_OnStarted(InputAction.CallbackContext context)
        {
            PlayerPerformedMovingForward?.Invoke();
        }

        private void MoveForward_OnCanceled(InputAction.CallbackContext context)
        {
            PlayerCanceledMovingForward?.Invoke();
        }

        public void Disable()
        {
            _playerInput.Player.MoveForward.performed -= MoveForward_OnStarted;
            _playerInput.Player.MoveForward.canceled -= MoveForward_OnCanceled;

            _playerInput.Player.Disable();
        }


        public float PlayerRotation()
        {
            return _playerInput.Player.Rotate.ReadValue<float>();
        }
    }
}