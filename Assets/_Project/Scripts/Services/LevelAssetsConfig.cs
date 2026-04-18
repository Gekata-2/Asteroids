using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Services
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Level Assets Config", fileName = "Level Assets Config", order = 0)]
    public class LevelAssetsConfig : ScriptableObject
    {
        [field: SerializeField] public List<string> UsedAssets { get; private set; }
    }
}