using UnityEngine;

namespace _Project.Scripts.Player
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Player Config", fileName = "Player Config", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; } = 3f;
        [field: SerializeField] public float RotationSpeed { get; private set; } = 270f;
        [field: SerializeField] public float ImmunityTimespan { get; private set; } = 5f;
    }
}