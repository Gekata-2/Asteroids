using System;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public interface IPlayerMovement
    {
        event Action PositionChanged;
        event Action SpeedChanged;
        event Action RotationChanged;

        Vector2 Position { get; }
        float Speed { get; }
        float Rotation { get; }
    }
}