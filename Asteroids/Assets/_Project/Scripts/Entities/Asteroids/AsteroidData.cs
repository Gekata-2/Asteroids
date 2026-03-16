using System;
using UnityEngine;

namespace _Project.Scripts.Entities.Asteroids
{
    [CreateAssetMenu(menuName = "Create Asteroid Data", fileName = "Asteroid Data", order = 0)]
    public class AsteroidData : EntityData
    {
        [Serializable]
        public class AsteroidSpeedData
        {
            public float min = 1f;
            public float max = 1f;
        }

        [Serializable]
        public class AsteroidTorqueData
        {
            public float min = 1f;
            public float max = 1f;
        }

        [SerializeField] private Asteroid prefab;
        [SerializeField] private AsteroidSpeedData speedData;
        [SerializeField] private AsteroidTorqueData torqueData;

        public Asteroid Prefab => prefab;
        public AsteroidSpeedData Speed => speedData;
        public AsteroidTorqueData Torque => torqueData;
    }
}