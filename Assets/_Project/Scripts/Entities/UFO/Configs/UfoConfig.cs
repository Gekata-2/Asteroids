using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Entities.UFO.Configs
{
    [CreateAssetMenu(menuName = "Scriptable Objects/UFO Config", fileName = "UFO Config", order = 0)]
    public class UfoConfig : EntityConfig
    {
        [field: SerializeField] public UfoMovementConfig Movement { get; private set; }
    }
}