using System;
using UnityEngine;

namespace Entities.Asteroids
{
    [Serializable]
    public class AsteroidsSplitData
    {
        [SerializeField] private AsteroidData data;
        [SerializeField] private int minCount = 1;
        [SerializeField] private int maxCount = 1;
        [SerializeField] private float size = 1;

        public AsteroidData Data => data;
        public int MinNewAsteroids => minCount;
        public int MaxNewAsteroids => maxCount;
        public float Size => size;
    }
}