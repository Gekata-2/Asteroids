using System;
using UnityEngine;

namespace _Project.Scripts.Player.Weapons
{
    [Serializable]
    public class MachineGunConfig
    {
        [field: SerializeField, Min(0.01f)] public float RateOfFire { get; private set; } = 2f;
        [field: SerializeField, Min(0f)] public float BulletSpeed { get; private set; } = 5f;
        [field: SerializeField, Min(0.01f)] public float BulletLifeTime { get; private set; } = 8f;

        public float FireCooldown => Mathf.Approximately(RateOfFire, 0f) ? float.MaxValue : 1f / RateOfFire;
    }
}