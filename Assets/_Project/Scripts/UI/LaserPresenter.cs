using System;
using _Project.Scripts.Player.Weapons.Laser;
using _Project.Scripts.Services.BeginGame;
using _Project.Scripts.UI.Laser;
using Zenject;

namespace _Project.Scripts.UI
{
    public class LaserPresenter : IInitializable, IDisposable, IAssetFetcher
    {
        private const string WINDOW_ASSET_NAME = "laser_ui";

        private readonly LaserModel _model;
        private LaserView _view;
        private readonly AssetsFactory _assetsFactory;

        public LaserPresenter(LaserModel model, AssetsFactory assetsFactory)
        {
            _model = model;
            _assetsFactory = assetsFactory;
        }

        public void Initialize()
        {
            _model.ChargesCountChanged += OnChargesCountChanged;
            _model.CooldownTimeLeftChanged += OnCooldownTimeLeftChanged;
        }

        public void FetchAssets()
        {
            _view = _assetsFactory.Create<LaserView>(WINDOW_ASSET_NAME);
            _view.SetChargesCount(_model.Charges);
            _view.SetProgress(_model.CooldownTimeLeft, _model.Cooldown);
        }

        private void OnCooldownTimeLeftChanged(float value)
            => _view.SetProgress(value, _model.Cooldown);

        private void OnChargesCountChanged(int count)
            => _view.SetChargesCount(count);

        public void Dispose()
        {
            _model.ChargesCountChanged -= OnChargesCountChanged;
            _model.CooldownTimeLeftChanged -= OnCooldownTimeLeftChanged;
        }
    }
}