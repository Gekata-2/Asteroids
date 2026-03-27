using System;
using UnityEngine;

namespace _Project.Scripts.Entities.UFO.Configs
{
    [Serializable]
    public class UfoMovementConfig
    {
        [field: SerializeField] public float SteeringSpeed { get; private set; } = 120f;
        [field: SerializeField] public float Speed { get; private set; } = 2f;
    }
}