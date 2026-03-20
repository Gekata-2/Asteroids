using UnityEngine;

namespace _Project.Scripts.Entities.UFO.Configs
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Create UFO Data", fileName = "UFO Data", order = 0)]
    public class UfoConfig : EntityConfig
    {
        [field: SerializeField] public UFO Prefab { get; private set; }
        [field: SerializeField] public UfoMovementConfig Movement { get; private set; }
    }
}