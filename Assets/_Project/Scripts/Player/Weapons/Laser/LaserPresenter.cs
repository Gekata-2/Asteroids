using System;
using _Project.Scripts.Player.Weapons.Laser.UI;
using _Project.Scripts.Services.AssetsManagement;
using _Project.Scripts.Services.BeginGame;
using Zenject;

namespace _Project.Scripts.Player.Weapons.Laser
{
    public class LaserPresenter : IInitializable, IDisposable, IAssetFetcher
    {
        private readonly LaserModel _model;
        private readonly AssetsFactory _assetsFactory;
        private LaserView _view;

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
            _view = _assetsFactory.Create<LaserView>(AssetsNames.GetName(Asset.LaserUI));
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