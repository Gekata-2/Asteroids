using _Project.Scripts.Entities.Asteroids;
using _Project.Scripts.Entities.Asteroids.Pools;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class AsteroidsInstaller : MonoInstaller
    {
   

        [SerializeField] private AsteroidPoolsConfig _poolsConfig;

        public override void InstallBindings()
        {
            Container.BindFactory<Object, Asteroid, AsteroidFactory>().FromFactory<PrefabFactory<Asteroid>>();
            Container.BindFactory<AsteroidPoolData, AsteroidPool, AsteroidPoolFactory>();

            Container.Bind<AsteroidPoolsConfig>().FromScriptableObject(_poolsConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<AsteroidPools>().AsSingle();
            Container.BindInterfacesAndSelfTo<AsteroidsConfigsRegistry>().AsSingle();

            Container.BindInterfacesAndSelfTo<AsteroidsSpawner>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}