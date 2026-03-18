using _Project.Scripts.Entities.Spawner;
using _Project.Scripts.Entities.UFO;
using UnityEngine;
using Zenject;

namespace _Project.Scripts._Installers
{
    public class UfosInstaller : MonoInstaller
    {
        [SerializeField] private UfoData ufoData;
        [SerializeField] private SimpleSpawnerConfig spawnerConfig;

        public override void InstallBindings()
        {
            Container.Bind<UfoData>().FromScriptableObject(ufoData).AsSingle();
            Container.Bind<SimpleSpawnerConfig>().FromScriptableObject(spawnerConfig).WhenInjectedInto<UfosSpawner>();
            
            Container.Bind<UfosSpawner>().FromComponentsInHierarchy().AsSingle();
            Container.Bind<UfosController>().FromComponentsInHierarchy().AsSingle();

            Container.Bind<ISpawnPositionPicker>()
                .To<RectangleSideSpawnPositionPicker>()
                .FromInstance(new RectangleSideSpawnPositionPicker(spawnerConfig.SpawnPositionSize, spawnerConfig.GizmosColor))
                .WhenInjectedInto<UfosSpawner>();
        }
    }
}