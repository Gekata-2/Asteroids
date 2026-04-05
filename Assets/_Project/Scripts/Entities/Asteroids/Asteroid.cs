using System;
using System.Collections.Generic;
using _Project.Scripts.Entities.Asteroids.Configs;
using _Project.Scripts.Entities.Asteroids.Pools;
using _Project.Scripts.Entities.UFO;
using ModestTree;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Entities.Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : EnemyEntity, IDamageVisitable, IDamageVisitor, IReinitializable
    {
        public event Action<Asteroid> Destroyed;
        public event Action<List<Asteroid>> CreatedDebris;

        private Queue<AsteroidsSplitConfig> SplitChain { get; set; }
        public AsteroidType Type { get; set; }

        public void Initialize(AsteroidsInitializationData initializationData)
        {
            InitializeData(initializationData.EntityConfig);
            SplitChain = initializationData.SplitChain;
            Rigidbody.AddTorque(initializationData.Torque);
            Rigidbody.linearVelocity = initializationData.Speed * initializationData.MoveDirection.normalized;
        }

        private AsteroidPools _pools;
        
        [Inject]
        private void Construct(AsteroidPools pools)
        {
            _pools = pools;
        }

        public void SetType(AsteroidType type)
            => Type = type;

        private void FixedUpdate()
        {
            HandlePositionChanger();
        }

        public void HandleLaser()
        {
            Destroyed?.Invoke(this);
        }

        public void HandleBullet()
        {
            gameObject.SetActive(false);
            
            if (!SplitChain.IsEmpty())
                SpawnAsteroidsFromSplitAtPosition(SplitChain, Rigidbody.position);
            
            Destroyed?.Invoke(this);
        }

        private void SpawnAsteroidsFromSplitAtPosition(Queue<AsteroidsSplitConfig> asteroidsChainRemainder,
            Vector2 position)
        {
            AsteroidsSplitConfig split = asteroidsChainRemainder.Dequeue();
            AsteroidConfig asteroidConfig = split.Config;

            int newAsteroidsCount = Random.Range(split.MinNewAsteroids, split.MaxNewAsteroids + 1);

            List<Asteroid> newAsteroids = new List<Asteroid>(newAsteroidsCount);

            for (int i = 0; i < newAsteroidsCount; i++)
            {
                Asteroid asteroid = _pools.Get(asteroidConfig.AsteroidType);
                asteroid.transform.position = position;
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
            if (other.gameObject.TryGetComponent(out IDamageVisitable visitable))
                visitable.Accept(this);
        }

        public override void Pause()
            => Rigidbody.simulated = false;

        public override void Resume()
            => Rigidbody.simulated = true;

        public void Accept(IDamageVisitor visitor)
            => visitor.Visit(this);

        public void Visit(Player.Player player)
            => player.Die();

        public void Visit(Asteroid asteroid)
        {
        }

        public void Visit(Ufo ufo)
        {
        }

        public void Reinitialize()
        {
            Rigidbody.simulated = false;
            Rigidbody.linearVelocity = Vector2.zero;
            Rigidbody.angularVelocity = 0f;
            Rigidbody.simulated = true;
        }
    }
}