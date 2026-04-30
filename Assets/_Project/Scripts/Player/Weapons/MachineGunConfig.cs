using System;
using UnityEngine;

namespace _Project.Scripts.Player.Weapons
{
    [Serializable]
    public class MachineGunConfig
    {
        [field: SerializeField, Min(0.01f)] public float RateOfFire { get; set; } = 2f;
        [field: SerializeField, Min(0f)] public float BulletSpeed { get; set; } = 5f;
        [field: SerializeField, Min(0.01f)] public float BulletLifeTime { get; set; } = 8f;

        public float FireCooldown => Mathf.Approximately(RateOfFire, 0f) ? float.MaxValue : 1f / RateOfFire;
    }
}