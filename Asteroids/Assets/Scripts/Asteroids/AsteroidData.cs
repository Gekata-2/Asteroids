using System;
using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Create Asteroid Data", fileName = "Asteroid Data", order = 0)]
    public class AsteroidData : ScriptableObject
    {
        [Serializable]
        public class AsteroidSpeedData
        {
            public float min = 1f;
            public float max = 1f;
        }

        [SerializeField] private Asteroid prefab;
        [SerializeField] private AsteroidSpeedData speedData;

        public Asteroid Prefab => prefab;
        public AsteroidSpeedData Speed => speedData;
    }
}