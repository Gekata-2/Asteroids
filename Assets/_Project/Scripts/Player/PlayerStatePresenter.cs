using System;
using _Project.Scripts.UI;

namespace _Project.Scripts.Player
{
    public class PlayerStatePresenter :  IDisposable
    {
        private readonly PlayerStateView _view;
        private IPlayerMovement _playerModel;

        public PlayerStatePresenter(PlayerStateView view)
        {
            _view = view;
        }

        public void SetPlayerModel(IPlayerMovement playerMovement)
        {
            _playerModel = playerMovement;

            _playerModel.PositionChanged += OnPositionChanged;
            _playerModel.RotationChanged += OnRotationChanged;
            _playerModel.SpeedChanged += OnSpeedChanged;

            _view.Initialize(_playerModel.Position, _playerModel.Rotation, _playerModel.Speed);
        }

        private void OnPositionChanged()
            => _view.SetPosition(_playerModel.Position);

        private void OnRotationChanged()
            => _view.SetAngle(_playerModel.Rotation);

        private void OnSpeedChanged()
            => _view.SetSpeed(_playerModel.Speed);

        public void Dispose()
        {
            _playerModel.PositionChanged -= OnPositionChanged;
            _playerModel.RotationChanged -= OnRotationChanged;
            _playerModel.SpeedChanged -= OnSpeedChanged;
        }
    }
}