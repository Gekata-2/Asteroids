using System;
using UnityEngine.InputSystem;

namespace Player
{
    public interface IInput
    {
        event Action PlayerPerformedMovingForward;
        event Action PlayerCanceledMovingForward;
        event Action ShootMachineGunPerformed;
        event Action ShootMachineGunCanceled;
        event Action ShootLaserPerformed;
        event Action PausePerformed;


        float PlayerRotation();

        void Enable();
        void Disable();
    }


    public class InputHandler : IInput
    {
        public event Action PlayerPerformedMovingForward;
        public event Action PlayerCanceledMovingForward;
        public event Action ShootMachineGunPerformed;
        public event Action ShootMachineGunCanceled;
        public event Action ShootLaserPerformed;
        public event Action PausePerformed;

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

            _playerInput.Player.ShootLaser.performed += ShootLaser_OnPerformed;
            _playerInput.Player.ShootMachineGun.performed += ShootMachineGun_OnPerformed;
            _playerInput.Player.ShootMachineGun.canceled += ShootMachineGun_OnCanceled;

            _playerInput.Player.Pause.performed += Pause_OnPerformed;
        }

        private void Pause_OnPerformed(InputAction.CallbackContext context)
        {
            PausePerformed?.Invoke();
        }

        private void ShootLaser_OnPerformed(InputAction.CallbackContext context)
        {
            ShootLaserPerformed?.Invoke();
        }

        private void ShootMachineGun_OnPerformed(InputAction.CallbackContext context)
        {
            ShootMachineGunPerformed?.Invoke();
        }

        private void ShootMachineGun_OnCanceled(InputAction.CallbackContext context)
        {
            ShootMachineGunCanceled?.Invoke();
        }

        private void MoveForward_OnStarted(InputAction.CallbackContext context)
        {
            PlayerPerformedMovingForward?.Invoke();
        }

        private void MoveForward_OnCanceled(InputAction.CallbackContext context)
        {
            PlayerCanceledMovingForward?.Invoke();
        }

        public float PlayerRotation() 
            => _playerInput.Player.Rotate.ReadValue<float>();

        public void Disable()
        {
            _playerInput.Player.Disable();

            _playerInput.Player.MoveForward.performed -= MoveForward_OnStarted;
            _playerInput.Player.MoveForward.canceled -= MoveForward_OnCanceled;

            _playerInput.Player.ShootLaser.performed -= ShootLaser_OnPerformed;
            _playerInput.Player.ShootMachineGun.performed -= ShootMachineGun_OnPerformed;
            _playerInput.Player.ShootMachineGun.canceled -= ShootMachineGun_OnCanceled;

            _playerInput.Player.Pause.performed -= Pause_OnPerformed;
        }
    }
}