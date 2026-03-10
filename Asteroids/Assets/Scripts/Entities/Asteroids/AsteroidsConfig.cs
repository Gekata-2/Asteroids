using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Asteroids
{
    [Serializable]
    public class AsteroidsChainData
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

    [CreateAssetMenu(menuName = "Create Asteroids Config", fileName = "Asteroids Config", order = 0)]
    public class AsteroidsConfig : ScriptableObject
    {
        [SerializeField] private List<AsteroidData> asteroidsData;
        [SerializeField] private List<AsteroidsChainData> chain;

        public List<AsteroidsChainData> Chain => chain;
    }
}