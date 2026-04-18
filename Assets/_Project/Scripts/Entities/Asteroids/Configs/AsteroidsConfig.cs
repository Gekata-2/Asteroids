using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Entities.Asteroids.Configs
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Asteroids Config", fileName = "Asteroids Config", order = 0)]
    public class AsteroidsConfig : ScriptableObject
    {
        [field: SerializeField] public List<AsteroidsSplitConfig> Chain { get; private set; }
    }
}