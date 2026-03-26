using System;
using System.Collections.Generic;
using _Project.Scripts.Entities.Asteroids.Configs;
using _Project.Scripts.Player;
using ModestTree;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Entities.Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : Entity, IDamageVisitable, IDamageVisitor
    {
        public event Action<Asteroid> Destroyed;
        public event Action<List<Asteroid>> CreatedDebris;

        public Queue<AsteroidsSplitConfig> SplitChain { get; private set; }

        public void Initialize(AsteroidsInitializationData initializationData)
        {
            InitializeData(initializationData.EntityConfig);
            SplitChain = initializationData.SplitChain;
            Rigidbody.AddTorque(initializationData.Torque);
            Rigidbody.linearVelocity = initializationData.Speed * initializationData.MoveDirection.normalized;
        }

        private void FixedUpdate()
        {
            HandlePositionChanger();
        }

        public void HandleLaser()
        {
            Destroy(gameObject);
            Destroyed?.Invoke(this);
        }

        public void HandleBullet()
        {
            Destroy(gameObject);

            if (!SplitChain.IsEmpty())
                SpawnAsteroidsFromSplitAtPosition(SplitChain, Rigidbody.position);

            Destroyed?.Invoke(this);
        }

        private void SpawnAsteroidsFromSplitAtPosition(Queue<AsteroidsSplitConfig> asteroidsChainRemainder,
            Vector3 position)
        {
            AsteroidsSplitConfig split = asteroidsChainRemainder.Dequeue();
            AsteroidConfig asteroidConfig = split.Config;

            int newAsteroidsCount = Random.Range(split.MinNewAsteroids, split.MaxNewAsteroids + 1);

            List<Asteroid> newAsteroids = new List<Asteroid>(newAsteroidsCount);

            for (int i = 0; i < newAsteroidsCount; i++)
            {
                Asteroid asteroid = Instantiate(asteroidConfig.Prefab, position, Quaternion.identity);
                asteroid.Initialize(
                    new AsteroidsInitializationData(
                        GetAsteroidSpeed(asteroidConfig.Speed),
                        GetAsteroidRandomDirection(),
                        GetAsteroidTorque(asteroidConfig.Torque),
                        asteroidsChainRemainder,
                        asteroidConfig));
                asteroid.transform.localScale = Vector3.one * split.Size;
                newAsteroids.Add(asteroid);
            }

            CreatedDebris?.Invoke(newAsteroids);
        }

        private float GetAsteroidSpeed(AsteroidSpeedConfig speedConfig) =>
            Random.Range(speedConfig.Min, speedConfig.Max);

        private Vector2 GetAsteroidRandomDirection()
            => Random.insideUnitCircle.normalized;

        private float GetAsteroidTorque(AsteroidTorqueConfig torqueConfig)
            => Random.Range(torqueConfig.Min, torqueConfig.Max);

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out PlayerHealth playerHealth))
                playerHealth.Accept(this);
        }

        public override void Pause()
            => Rigidbody.simulated = false;

        public override void Resume()
            => Rigidbody.simulated = true;

        public void Accept(IDamageVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void Visit(PlayerHealth playerHealth)
        {
            playerHealth.Die();
        }

        public void Visit(Asteroid asteroid)
        {
        }

        public void Visit(UFO.UFO ufo)
        {
        }
    }
}