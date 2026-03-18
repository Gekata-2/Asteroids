using System;

namespace _Project.Scripts.Level.GameSession
{
    public abstract class GameSessionModel
    {
        public abstract event Action ScoreChanged;
        public abstract int Score { get; protected set; }
        public abstract void AddScore(int value);
        public abstract void SetScore(int value);
    }
}