using UI;
using UnityEngine;

namespace Player
{
    public abstract class PlayerModel
    {
        protected readonly ScoreView _scoreView;

        protected PlayerModel(ScoreView scoreView)
        {
            _scoreView = scoreView;
            _scoreView.SetScore(0);
        }

        public abstract Vector2 Position { get; }
        public abstract float Speed { get; }
        public abstract float Angle { get; }
        public abstract int Score { get; protected set; }
        public abstract void SetPlayer(IPlayerMovement player);
        public abstract void AddScore(int value);
    }
}