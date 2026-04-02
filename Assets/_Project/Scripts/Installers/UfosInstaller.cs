using _Project.Scripts.Entities;
using _Project.Scripts.Entities.Spawner;
using _Project.Scripts.Entities.UFO;
using _Project.Scripts.Entities.UFO.Configs;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class UfosInstaller : MonoInstaller
    {
        [SerializeField] private UfoConfig ufoConfig;
        [SerializeField] private SimpleSpawnerConfig spawnerConfig;

        public override void InstallBindings()
        {
            Container.Bind<UfoConfig>().FromScriptableObject(ufoConfig).AsSingle();
            Container.Bind<SimpleSpawnerConfig>().FromScriptableObject(spawnerConfig).WhenInjectedInto<UfosSpawner>();

            Container.BindInterfacesAndSelfTo<UfosSpawner>().FromComponentsInHierarchy().AsSingle();
            Container.Bind<EntitiesController>().To<UfosController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<RectangleSideSpawnPositionPicker>()
                .WithArguments(spawnerConfig.SpawnPositionSize, spawnerConfig.GizmosColor)
                .WhenInjectedInto<UfosSpawner>();
        }
    }
}