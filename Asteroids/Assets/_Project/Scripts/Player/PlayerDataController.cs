using UI;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerDataController : IFixedTickable, IInitializable
    {
        private readonly PlayerStateView _view;
        private readonly PlayerModel _model;

        public PlayerDataController(PlayerStateView view, PlayerModel model)
        {
            _view = view;
            _model = model;
        }

        public void Initialize()
        {
            _view.SetPosition(Vector2.zero);
            _view.SetAngle(0);
            _view.SetSpeed(0);
        }

        public void FixedTick()
        {
            UpdateView();
        }

        private void UpdateView()
        {
            Vector2 pos = _model.Position;
            _view.SetPosition(pos);
            _view.SetAngle(_model.Angle);
            _view.SetSpeed(_model.Speed);
        }
    }
}