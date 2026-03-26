using System;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Player
{
    public interface IInput
    {
        event Action PlayerPerformedMovingForward;
        event Action PlayerCanceledMovingForward;
        event Action ShootMachineGunPerformed;
        event Action ShootMachineGunCanceled;
        event Action ShootLaserPerformed;
        event Action PausePerformed;
        event Action SubmitPerformed;
        event Action CancelPerformed;


        float PlayerRotation();

        void Enable();
        void Disable();

        void SetPlayerActionsEnabled(bool isEnabled);
        void SetUIActionsEnabled(bool isEnabled);
    }


    public class InputHandler : IInput
    {
        public event Action PlayerPerformedMovingForward;
        public event Action PlayerCanceledMovingForward;
        public event Action ShootMachineGunPerformed;
        public event Action ShootMachineGunCanceled;
        public event Action ShootLaserPerformed;
        public event Action PausePerformed;
        public event Action SubmitPerformed;
        public event Action CancelPerformed;

        private readonly PlayerInputActionMap _playerInput;

        public InputHandler(PlayerInputActionMap playerInput)
        {
            _playerInput = playerInput;
        }

        public void Enable()
        {
            _playerInput.Player.Enable();

            _playerInput.Player.MoveForward.performed += OnMoveForwardPerformed;
            _playerInput.Player.MoveForward.canceled += OnMoveForwardCanceled;

            _playerInput.Player.ShootLaser.performed += OnShootLaserPerformed;
            
            _playerInput.Player.ShootMachineGun.performed += OnShootMachineGunPerformed;
            _playerInput.Player.ShootMachineGun.canceled += OnShootMachineGunCanceled;

            _playerInput.Player.Pause.performed += OnPausePerformed;

            _playerInput.UI.Submit.performed += OnSubmitPerformed;
            _playerInput.UI.Cancel.performed += OnCancelPerformed;
        }

        public void Disable()
        {
            _playerInput.Player.Disable();
            _playerInput.UI.Disable();
            
            _playerInput.Player.MoveForward.performed -= OnMoveForwardPerformed;
            _playerInput.Player.MoveForward.canceled -= OnMoveForwardCanceled;

            _playerInput.Player.ShootLaser.performed -= OnShootLaserPerformed;
            _playerInput.Player.ShootMachineGun.performed -= OnShootMachineGunPerformed;
            _playerInput.Player.ShootMachineGun.canceled -= OnShootMachineGunCanceled;

            _playerInput.Player.Pause.performed -= OnPausePerformed;

            _playerInput.UI.Submit.performed -= OnSubmitPerformed;
            _playerInput.UI.Cancel.performed -= OnCancelPerformed;
        }

        public void SetPlayerActionsEnabled(bool isEnabled)
        {
            if (isEnabled)
                _playerInput.Player.Enable();
            else
                _playerInput.Player.Disable();
        }

        public void SetUIActionsEnabled(bool isEnabled)
        {
            if (isEnabled)
                _playerInput.UI.Enable();
            else
                _playerInput.UI.Disable();
        }

        private void OnPausePerformed(InputAction.CallbackContext context)
            => PausePerformed?.Invoke();

        private void OnShootLaserPerformed(InputAction.CallbackContext context)
            => ShootLaserPerformed?.Invoke();

        private void OnShootMachineGunPerformed(InputAction.CallbackContext context)
            => ShootMachineGunPerformed?.Invoke();

        private void OnShootMachineGunCanceled(InputAction.CallbackContext context)
            => ShootMachineGunCanceled?.Invoke();

        private void OnMoveForwardPerformed(InputAction.CallbackContext context)
            => PlayerPerformedMovingForward?.Invoke();

        private void OnMoveForwardCanceled(InputAction.CallbackContext context)
            => PlayerCanceledMovingForward?.Invoke();

        private void OnSubmitPerformed(InputAction.CallbackContext context)
            => SubmitPerformed?.Invoke();

        private void OnCancelPerformed(InputAction.CallbackContext context)
            => CancelPerformed?.Invoke();

        public float PlayerRotation()
            => _playerInput.Player.Rotate.ReadValue<float>();
    }
}