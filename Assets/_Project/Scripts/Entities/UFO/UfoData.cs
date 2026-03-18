using System;
using UnityEngine;

namespace _Project.Scripts.Entities.UFO
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Create UFO Data", fileName = "UFO Data", order = 0)]
    public class UfoData : EntityData
    {
        [Serializable]
        public class MovementData
        {
            [field: SerializeField] public float SteeringSpeed { get; private set; } = 120f;
            [field: SerializeField] public float Speed { get; private set; } = 2f;
        }

        [field: SerializeField] public UFO Prefab { get; private set; }
        [field: SerializeField] public MovementData Movement { get; private set; }
    }
}