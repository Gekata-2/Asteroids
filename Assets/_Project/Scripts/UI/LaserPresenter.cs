using System;
using _Project.Scripts.Player.Weapons.Laser;
using _Project.Scripts.Services.AssetsProviding;
using _Project.Scripts.Services.BeginGame;
using _Project.Scripts.UI.Laser;
using Zenject;

namespace _Project.Scripts.UI
{
    public class LaserPresenter : IInitializable, IDisposable, IAssetFetcher
    {
        private readonly LaserModel _model;
        private readonly AssetsFactory _assetsFactory;
        private readonly AssetsNames _assetsNames;
        private LaserView _view;

        public LaserPresenter(LaserModel model, AssetsNames assetsNames, AssetsFactory assetsFactory)
        {
            _model = model;
            _assetsFactory = assetsFactory;
            _assetsNames = assetsNames;
        }

        public void Initialize()
        {
            _model.ChargesCountChanged += OnChargesCountChanged;
            _model.CooldownTimeLeftChanged += OnCooldownTimeLeftChanged;
        }

        public void FetchAssets()
        {
            _view = _assetsFactory.Create<LaserView>(_assetsNames.GetName(Asset.LaserUI));
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