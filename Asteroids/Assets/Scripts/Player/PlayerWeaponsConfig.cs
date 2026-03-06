using System;
using UnityEngine;

namespace Player
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

        public MachineGunConfig MachineGun;
    }
}