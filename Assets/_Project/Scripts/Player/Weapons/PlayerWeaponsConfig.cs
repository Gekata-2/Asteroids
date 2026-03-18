using System;
using UnityEngine;

namespace _Project.Scripts.Player.Weapons
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Create Player Weapons Config", fileName = "Player Weapons Config",
        order = 0)]
    public class PlayerWeaponsConfig : ScriptableObject
    {
        [Serializable]
        public class MachineGunConfig
        {
            [field: SerializeField, Min(0.01f)] public float RateOfFire { get; private set; } = 2f;
            [field: SerializeField, Min(0f)] public float BulletSpeed { get; private set; } = 5f;
            [field: SerializeField, Min(0.01f)] public float BulletLifeTime { get; private set; } = 8f;

            public float FireCooldown => Mathf.Approximately(RateOfFire, 0f) ? float.MaxValue : 1f / RateOfFire;
        }

        [Serializable]
        public class LaserConfig
        {
            [field: SerializeField] public float Cooldown { get; private set; } = 15f;
            [field: SerializeField] public float Lenght { get; private set; } = 4f;
            [field: SerializeField] public float Duration { get; private set; } = 2f;
            [field: SerializeField] public int Charges { get; private set; } = 1;
        }

        [field: SerializeField] public MachineGunConfig MachineGun { get; private set; }
        [field: SerializeField] public LaserConfig Laser { get; private set; }
    }
}