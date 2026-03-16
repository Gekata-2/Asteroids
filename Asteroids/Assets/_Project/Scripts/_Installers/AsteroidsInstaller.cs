using Entities.Asteroids;
using Entities.Spawner;
using UnityEngine;
using Zenject;

namespace _Installers
{
    public class AsteroidsInstaller : MonoInstaller
    {
        [SerializeField] private AsteroidsConfig asteroidsConfig;
        [SerializeField] private SimpleSpawnerConfig spawnerConfig;

        public override void InstallBindings()
        {
            Container.Bind<AsteroidsConfig>().FromScriptableObject(asteroidsConfig).AsSingle().NonLazy();
            Container.Bind<AsteroidsSpawner>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<SimpleSpawnerConfig>().FromScriptableObject(spawnerConfig).WhenInjectedInto<AsteroidsSpawner>().NonLazy();

            Container.Bind<ISpawnPositionPicker>()
                .To<RectangleSideSpawnPositionPicker>()
                .FromInstance(new RectangleSideSpawnPositionPicker(spawnerConfig.SpawnPositionSize, spawnerConfig.GizmosColor))
                .WhenInjectedInto<AsteroidsSpawner>().NonLazy();
        }
    }
}