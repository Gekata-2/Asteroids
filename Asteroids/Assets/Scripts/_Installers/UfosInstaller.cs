using Entities.Spawner;
using Entities.UFO;
using UnityEngine;
using Zenject;

namespace _Installers
{
    public class UfosInstaller : MonoInstaller
    {
        [SerializeField] private UfoData ufoData;
        [SerializeField] private SimpleSpawnerConfig spawnerConfig;

        public override void InstallBindings()
        {
            Container.Bind<UfosSpawner>().FromComponentsInHierarchy().AsSingle().NonLazy();
            Container.Bind<UfosController>().FromComponentsInHierarchy().AsSingle().NonLazy();
            Container.Bind<UfoData>().FromScriptableObject(ufoData).AsSingle().NonLazy();
            Container.Bind<SimpleSpawnerConfig>().FromScriptableObject(spawnerConfig).WhenInjectedInto<UfosSpawner>()
                .NonLazy();

            Container.Bind<ISpawnPositionPicker>()
                .To<RectangleSideSpawnPositionPicker>()
                .FromInstance(new RectangleSideSpawnPositionPicker(spawnerConfig.SpawnPositionSize, spawnerConfig.GizmosColor))
                .WhenInjectedInto<UfosSpawner>().NonLazy();
        }
    }
}