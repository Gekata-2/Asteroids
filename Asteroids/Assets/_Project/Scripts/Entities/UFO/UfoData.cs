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
            [SerializeField] private float steeringSpeed;
            [SerializeField] private float speed;
            public float SteeringSpeed=>steeringSpeed;
            public float Speed => speed;

        }
        
        [SerializeField] private UFO prefab;
        [SerializeField] private MovementData movement;
    
        public UFO Prefab => prefab;
        public MovementData Movement => movement;
    }
}