using System.Collections.Generic;
using UnityEngine;

namespace Entities.Asteroids
{
    [CreateAssetMenu(menuName = "Create Asteroids Config", fileName = "Asteroids Config", order = 0)]
    public class AsteroidsConfig : ScriptableObject
    {
        [SerializeField] private List<AsteroidsSplitData> chain;

        public List<AsteroidsSplitData> Chain => chain;
    }
}