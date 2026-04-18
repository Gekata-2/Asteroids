using System;
using _Project.Scripts.Services.BeginGame;
using _Project.Scripts.UI;

namespace _Project.Scripts.Player
{
    public class PlayerStatePresenter : IAssetFetcher, IDisposable
    {
        private const string WINDOW_ASSET_NAME = "player_state_ui";
        
        private readonly AssetsFactory _viewFactory;

        private PlayerMovement _playerModel;
        private PlayerStateView _view;

        public PlayerStatePresenter(AssetsFactory viewFactory)
        {
            _viewFactory = viewFactory;
        }

        public void FetchAssets()
        {
            _view = _viewFactory.Create<PlayerStateView>(WINDOW_ASSET_NAME);
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
            _playerModel.PositionChanged -= OnPositionChanged;
            _playerModel.RotationChanged -= OnRotationChanged;
            _playerModel.SpeedChanged -= OnSpeedChanged;
        }
    }
}