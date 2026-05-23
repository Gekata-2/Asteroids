using System;
using _Project.Scripts.Player.UI;
using _Project.Scripts.Services.AssetsManagement;

namespace _Project.Scripts.Player
{
    public class PlayerStatePresenter : IAssetFetcher, IDisposable
    {
    
        private readonly PlayerStateViewFactory _viewFactory;
        private PlayerMovement _playerModel;
        private PlayerStateView _view;

        public PlayerStatePresenter(PlayerStateViewFactory viewFactory)
        {
            _viewFactory = viewFactory;
        }

        public void FetchAssets()
        {
            _view = _viewFactory.Create();
            if (_playerModel != null)
                _view.Initialize(_playerModel.Position, _playerModel.Rotation, _playerModel.Speed);
        }

        public void SetPlayerModel(PlayerMovement playerMovement)
        {
            _playerModel = playerMovement;

            _playerModel.PositionChanged += OnPositionChanged;
            _playerModel.RotationChanged += OnRotationChanged;
            _playerModel.SpeedChanged += OnSpeedChanged;

            if (_view != null)
                _view.Initialize(_playerModel.Position, _playerModel.Rotation, _playerModel.Speed);
        }

        private void OnPositionChanged()
        {
            if (_view != null)
                _view.SetPosition(_playerModel.Position);
        }

        private void OnRotationChanged()
        {
            if (_view != null)
                _view.SetAngle(_playerModel.Rotation);
        }

        private void OnSpeedChanged()
        {
            if (_view != null)
                _view.SetSpeed(_playerModel.Speed);
        }

        public void Dispose()
        {
            if (_playerModel == null) return;
            
            _playerModel.PositionChanged -= OnPositionChanged;
            _playerModel.RotationChanged -= OnRotationChanged;
            _playerModel.SpeedChanged -= OnSpeedChanged;
        }
    }
}