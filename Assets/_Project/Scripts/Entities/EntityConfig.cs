using UnityEngine;

namespace _Project.Scripts.Entities
{
    public class EntityConfig : ScriptableObject
    {
        [field: SerializeField] public int Score { get; private set; } = 1;
    }
}