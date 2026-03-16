using UnityEngine;

namespace _Project.Scripts.Player
{
    public interface IPlayerMovement
    {
        Vector2 Position { get; }
        float Speed { get; }
        float Rotation { get; }
    }
}