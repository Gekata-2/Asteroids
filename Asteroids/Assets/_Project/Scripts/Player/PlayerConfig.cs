using UnityEngine;

namespace _Project.Scripts.Player
{
    [CreateAssetMenu(menuName = "Create Player Config", fileName = "Player Config", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private float speed;
        [SerializeField] private float rotationSpeed;

        public float Speed => speed;
        public float RotationSpeed => rotationSpeed;
    }
}