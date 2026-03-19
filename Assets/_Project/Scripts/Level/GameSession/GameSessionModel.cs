using System;

namespace _Project.Scripts.Level.GameSession
{
    public class GameSessionModel
    {
        public event Action ScoreChanged;
        public int Score { get; private set; }

        public void AddScore(int value)
        {
            if (value == 0)
                return;

            Score += value;
            if (Score <= 0)
                Score = 0;

            ScoreChanged?.Invoke();
        }

        public void SetScore(int value)
        {
            if (value < 0)
                return;

            Score = value;
            ScoreChanged?.Invoke();
        }
    }
}