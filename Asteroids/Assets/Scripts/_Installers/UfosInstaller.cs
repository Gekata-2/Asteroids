using Entities.UFO;
using UnityEngine;
using Zenject;

namespace _Installers
{
    public class UfosInstaller : MonoInstaller
    {
        [SerializeField] private UfoData ufoData;
        [SerializeField] private UfospawnerConfig spawnerConfig;

        public override void InstallBindings()
        {
            Container.Bind<UfosSpawner>().FromComponentsInHierarchy().AsSingle().NonLazy();
            Container.Bind<UfosController>().FromComponentsInHierarchy().AsSingle().NonLazy();
            Container.Bind<UfoData>().FromScriptableObject(ufoData).AsSingle().NonLazy();
            Container.Bind<UfospawnerConfig>().FromScriptableObject(spawnerConfig).AsSingle().NonLazy();
        }
    }
}