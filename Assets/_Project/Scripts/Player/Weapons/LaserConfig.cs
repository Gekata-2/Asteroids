using System;
using UnityEngine;

namespace _Project.Scripts.Player.Weapons
{
    [Serializable]
    public class LaserConfig
    {
        [field: SerializeField] public float Cooldown { get; set; } = 15f;
        [field: SerializeField] public float Lenght { get; set; } = 4f;
        [field: SerializeField] public float Duration { get; set; } = 2f;
        [field: SerializeField] public int Charges { get; set; } = 1;
    }
}