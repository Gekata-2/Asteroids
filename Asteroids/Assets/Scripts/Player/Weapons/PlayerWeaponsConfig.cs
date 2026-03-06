using System;
using UnityEngine;

namespace Player.Weapons
{
    [CreateAssetMenu(menuName = "Create Player Weapons Config", fileName = "Player Weapons Config", order = 0)]
    public class PlayerWeaponsConfig : ScriptableObject
    {
        [Serializable]
        public class MachineGunConfig
        {
            [SerializeField, Min(0.01f)] private float rateOfFire;
            [SerializeField, Min(0f)] private float bulletSpeed;

            public float RateOfFire => rateOfFire;
            public float FireCooldown => Mathf.Approximately(rateOfFire, 0f) ? float.MaxValue : 1f / rateOfFire;

            public float BulletSpeed => bulletSpeed;
        }

        [Serializable]
        public class LaserConfig
        {
            [SerializeField] private float cooldown = 15f;
            [SerializeField] private float lenght = 4f;
            [SerializeField] private float duration = 2f;
            [SerializeField] private int charges = 1;
            public float Cooldown => cooldown;
            public float Lenght => lenght;
            public float Duration => duration;
            public int Charges => charges;
        }

        public MachineGunConfig MachineGun;
        public LaserConfig Laser;
    }
}