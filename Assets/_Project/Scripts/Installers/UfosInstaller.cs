using _Project.Scripts.Entities.Spawner;
using _Project.Scripts.Entities.UFO;
using _Project.Scripts.Entities.UFO.Configs;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class UfosInstaller : MonoInstaller
    {
        [SerializeField] private UfoConfig _ufoConfig;
        [SerializeField] private SimpleSpawnerConfig _spawnerConfig;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<UfoFactory>().AsSingle();

            Container.Bind<UfoConfig>().FromScriptableObject(_ufoConfig).AsSingle();
            Container.Bind<SimpleSpawnerConfig>().FromScriptableObject(_spawnerConfig).WhenInjectedInto<UfosSpawner>();

            Container.BindInterfacesAndSelfTo<UfosSpawner>().FromComponentsInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<UfosController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<RectangleSideSpawnPositionPicker>()
                .WithArguments(_spawnerConfig.SpawnPositionSize, _spawnerConfig.GizmosColor)
                .WhenInjectedInto<UfosSpawner>();
        }
    }
}