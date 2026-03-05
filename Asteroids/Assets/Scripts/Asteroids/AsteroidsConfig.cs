using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Create Asteroids Config", fileName = "Asteroids Config", order = 0)]
    public class AsteroidsConfig : ScriptableObject
    {
        [Serializable]
        public class AsteroidSpeedData
        {
            public float min = 1f;
            public float max = 1f;
        }

        [Serializable]
        public class AsteroidData
        {
            [SerializeField] private Asteroid prefab;
            [SerializeField] private AsteroidSpeedData speedData;

            public Asteroid Prefab => prefab;
            public AsteroidSpeedData Speed => speedData;
        }

        [SerializeField] private List<AsteroidData> asteroidsData;

        public List<AsteroidData> AsteroidsData => asteroidsData;
    }
}