using System;
using _Project.Scripts.Player.Weapons.Laser.UI;
using _Project.Scripts.Services.AssetsManagement;
using Zenject;

namespace _Project.Scripts.Player.Weapons.Laser
{
    public class LaserPresenter : IInitializable, IDisposable, IAssetFetcher
    {
        private readonly LaserModel _model;
        private readonly LaserViewFactory _viewFactory;
        private LaserView _view;

        public LaserPresenter(LaserModel model, LaserViewFactory viewFactory)
        {
            _model = model;
            _viewFactory = viewFactory;
        }

        public void Initialize()
        {
            _model.ChargesCountChanged += OnChargesCountChanged;
            _model.CooldownTimeLeftChanged += OnCooldownTimeLeftChanged;
        }

        public void FetchAssets()
        {
            _view = _viewFactory.Create();
            _view.SetChargesCount(_model.Charges);
            _view.SetProgress(_model.CooldownTimeLeft, _model.Cooldown);
        }

        private void OnCooldownTimeLeftChanged(float value)
        {
            if (_view != null)
                _view.SetProgress(value, _model.Cooldown);
        }

        private void OnChargesCountChanged(int count)
        {
            if (_view != null) 
                _view?.SetChargesCount(count);
        }

        public void Dispose()
        {
            _model.ChargesCountChanged -= OnChargesCountChanged;
            _model.CooldownTimeLeftChanged -= OnCooldownTimeLeftChanged;
        }
    }
}