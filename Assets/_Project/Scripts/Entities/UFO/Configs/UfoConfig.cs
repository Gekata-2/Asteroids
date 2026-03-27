using UnityEngine;

namespace _Project.Scripts.Entities.UFO.Configs
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Create UFO Config", fileName = "UFO Config", order = 0)]
    public class UfoConfig : EntityConfig
    {
        [field: SerializeField] public Ufo Prefab { get; private set; }
        [field: SerializeField] public UfoMovementConfig Movement { get; private set; }
    }
}