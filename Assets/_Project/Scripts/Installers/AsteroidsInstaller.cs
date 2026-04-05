using _Project.Scripts.Entities.Asteroids;
using _Project.Scripts.Entities.Asteroids.Configs;
using _Project.Scripts.Entities.Asteroids.Pools;
using _Project.Scripts.Entities.Factories;
using _Project.Scripts.Entities.Spawner;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class AsteroidsInstaller : MonoInstaller
    {
        [SerializeField] private AsteroidsConfig asteroidsConfig;
        [SerializeField] private SimpleSpawnerConfig spawnerConfig;
        [SerializeField] private AsteroidPoolsConfig poolsConfig;

        public override void InstallBindings()
        {
            Container.BindFactory<Object, Asteroid, AsteroidFactory>().FromFactory<PrefabFactory<Asteroid>>();
            Container.BindFactory<AsteroidPoolData, AsteroidPool, AsteroidPoolFactory>();

            Container.Bind<AsteroidPoolsConfig>().FromScriptableObject(poolsConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<AsteroidPools>().AsSingle();

            Container.Bind<AsteroidsConfig>().FromScriptableObject(asteroidsConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<AsteroidsSpawner>().FromComponentInHierarchy().AsSingle();

            Container.Bind<SimpleSpawnerConfig>().FromScriptableObject(spawnerConfig)
                .WhenInjectedInto<AsteroidsSpawner>();

            Container.Bind<RectangleSideSpawnPositionPicker>()
                .WithArguments(spawnerConfig.SpawnPositionSize, spawnerConfig.GizmosColor)
                .WhenInjectedInto<AsteroidsSpawner>();
        }
    }
}