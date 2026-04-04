using _Project.Scripts.Entities.Factories;
using _Project.Scripts.Entities.Spawner;
using _Project.Scripts.Entities.UFO;
using _Project.Scripts.Entities.UFO.Configs;
using _Project.Scripts.Player;
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
            Container.BindFactory<EnemyTarget, Ufo, UfoFactory>().FromFactory<CustomUfoFactory>();
            Container.Bind<UfoConfig>().FromScriptableObject(ufoConfig).AsSingle();
            Container.Bind<SimpleSpawnerConfig>().FromScriptableObject(spawnerConfig).WhenInjectedInto<UfosSpawner>();

            Container.BindInterfacesAndSelfTo<UfosSpawner>().FromComponentsInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<UfosController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<RectangleSideSpawnPositionPicker>()
                .WithArguments(spawnerConfig.SpawnPositionSize, spawnerConfig.GizmosColor)
                .WhenInjectedInto<CustomUfoFactory>();
        }
    }
}