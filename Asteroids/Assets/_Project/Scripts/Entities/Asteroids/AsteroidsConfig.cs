using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Entities.Asteroids
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Create Asteroids Config", fileName = "Asteroids Config", order = 0)]
    public class AsteroidsConfig : ScriptableObject
    {
        [SerializeField] private List<AsteroidsSplitData> chain;

        public List<AsteroidsSplitData> Chain => chain;
    }
}