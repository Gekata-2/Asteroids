using System.Collections.Generic;
using _Project.Scripts.Sfx;
using _Project.Scripts.Vfx;
using UnityEngine;

namespace _Project.Scripts.Services.AssetsManagement
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Level Assets Config", fileName = "Level Assets Config", order = 0)]
    public class AssetBundleConfig : ScriptableObject
    {
        [field: SerializeField] public List<Asset> UsedAssets { get; private set; }
        [field: SerializeField] public List<SFX> UsedSfx { get; private set; }
        [field: SerializeField] public List<VFX> UsedVfx { get; private set; }
    }
}