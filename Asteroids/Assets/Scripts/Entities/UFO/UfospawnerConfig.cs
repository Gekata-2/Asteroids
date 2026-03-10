using UnityEngine;

namespace Entities.UFO
{
    [CreateAssetMenu(menuName = "Create UFO Spawner Config", fileName = "UFO Spawner Config", order = 0)]
    public class UfospawnerConfig : ScriptableObject
    {
        [SerializeField] private float minInterval = 1f;
        [SerializeField] private float maxInterval = 3f;
        [SerializeField] private float startDelay = 2f;

        public float MinInterval => minInterval;
        public float MaxInterval => maxInterval;
        public float StartDelay => startDelay;
    }
}