using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerState : PlayerModel
    {
        private IPlayerMovement _player;

        public PlayerState(ScoreView scoreView) : base(scoreView)
        {
        }

        public override int Score { get; protected set; }
        public override Vector2 Position => _player?.Position ?? default;
        public override float Speed => _player?.Speed ?? default;
        public override float Angle => _player?.Rotation ?? default;

        public override void SetPlayer(IPlayerMovement player)
            => _player = player;

        public override void AddScore(int value)
        {
            Score += value;
            if (Score <= 0)
                Score = 0;
            _scoreView.SetScore(Score);
        }
    }
}