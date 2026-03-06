using UnityEngine;

namespace Player
{
    public abstract class PlayerModel
    {
        public abstract void SetPlayer(IPlayerMovement player);
        public abstract Vector2 Position { get; }
        public abstract float Speed { get; }
        public abstract float Angle { get; }
    }
}