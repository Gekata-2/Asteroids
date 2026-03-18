using System;
using UnityEngine;

namespace _Project.Scripts.Entities.Asteroids
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Create Asteroid Data", fileName = "Asteroid Data", order = 0)]
    public class AsteroidData : EntityData
    {
        [Serializable]
        public class AsteroidSpeedData
        {
            [field: SerializeField] public float Min { get; private set; } = 0.75f;
            [field: SerializeField] public float Max { get; private set; } = 1.25f;
        }

        [Serializable]
        public class AsteroidTorqueData
        {
            [field: SerializeField] public float Min { get; private set; } = 3f;
            [field: SerializeField] public float Max { get; private set; } = -3f;
        }

        [field: SerializeField] public Asteroid Prefab { get; private set; }
        [field: SerializeField] public AsteroidSpeedData Speed { get; private set; }
        [field: SerializeField] public AsteroidTorqueData Torque { get; private set; }
    }
}