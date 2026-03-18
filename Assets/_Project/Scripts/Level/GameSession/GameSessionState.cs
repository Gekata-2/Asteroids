using System;

namespace _Project.Scripts.Level.GameSession
{
    public class GameSessionState : GameSessionModel
    {
        public override event Action ScoreChanged;
        public override int Score { get; protected set; }

        public override void AddScore(int value)
        {
            if (value == 0)
                return;

            Score += value;
            if (Score <= 0)
                Score = 0;

            ScoreChanged?.Invoke();
        }

        public override void SetScore(int value)
        {
            if (value < 0)
                return;

            Score = value;
            ScoreChanged?.Invoke();
        }
    }
}