using System.Collections.Generic;
using _Project.Scripts.Entities.Asteroids.Configs;
using UnityEngine;

namespace _Project.Scripts.Entities.Asteroids
{
    public struct AsteroidsInitializationData
    {
        public AsteroidsInitializationData(float speed, Vector2 moveDirection, float torque,
            Queue<AsteroidsSplitConfig> splitChain, EntityConfig entityConfig)
        {
            Speed = speed;
            MoveDirection = moveDirection;
            Torque = torque;
            SplitChain = splitChain;
            EntityConfig = entityConfig;
        }

        public float Speed { get; }
        public Vector2 MoveDirection { get; }
        public float Torque { get; }
        public Queue<AsteroidsSplitConfig> SplitChain { get; }
        public EntityConfig EntityConfig { get; }
    }
}