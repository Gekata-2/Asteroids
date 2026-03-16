using _Project.Scripts.Entities;
using _Project.Scripts.LevelBounds;
using _Project.Scripts.Services.Awards;
using UnityEngine;
using Zenject;

namespace _Project.Scripts._Installers
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private GameSettings gameSettings;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EntitiesContainer>().FromNew().AsSingle().NonLazy();
            Container.Bind<LevelBounds.LevelBounds>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<IPositionWrapper>().To<InvertedClampedEntityPositionWrapper>().FromNew().AsSingle()
                .NonLazy();
            Container.BindInterfacesAndSelfTo<EntityOutOfBoundsController>().FromNew().AsSingle().NonLazy();
            Container.Bind<GameSettings>().FromScriptableObject(gameSettings).AsSingle().NonLazy();
        }
    }
}