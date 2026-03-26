using System;
using _Project.Scripts.Player.Weapons.Laser;
using _Project.Scripts.UI.Laser;
using Zenject;

namespace _Project.Scripts.UI
{
    public class LaserPresenter : IInitializable, IDisposable
    {
        private readonly LaserModel _model;
        private readonly LaserView _view;

        public LaserPresenter(LaserModel model, LaserView view)
        {
            _model = model;
            _view = view;
        }

        public void Initialize()
        {
            _model.ChargesCountChanged += OnChargesCountChanged;
            _model.CooldownTimeLeftChanged += OnCooldownTimeLeftChanged;
            _view.SetChargesCount(_model.Charges);
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