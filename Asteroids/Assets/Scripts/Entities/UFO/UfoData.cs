using UnityEngine;

namespace Entities.UFO
{
    [CreateAssetMenu(menuName = "Create UFO Data", fileName = "UFO Data", order = 0)]
    public class UfoData : EntityData
    {
        [SerializeField] private UFO prefab;
        public UFO Prefab => prefab;
    }
}