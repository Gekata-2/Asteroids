using Asteroids;
using UnityEngine;
using Zenject;

namespace _Installers
{
    public class AsteroidsInstaller : MonoInstaller
    {
        [SerializeField] private AsteroidsConfig asteroidsConfig;
        [SerializeField] private AsteroidsSpawnerConfig asteroidsSpawnerConfig;

        public override void InstallBindings()
        {
            Container.Bind<AsteroidsSpawnerConfig>().FromScriptableObject(asteroidsSpawnerConfig).AsSingle().NonLazy();
            Container.Bind<AsteroidsConfig>().FromScriptableObject(asteroidsConfig).AsSingle().NonLazy();
            Container.Bind<AsteroidsSpawner>().FromComponentInHierarchy().AsSingle().NonLazy();
        }
    }
}