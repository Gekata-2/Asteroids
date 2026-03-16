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
            Container.Bind<GameSettings>().FromScriptableObject(gameSettings).AsSingle();
            
            Container.BindInterfacesAndSelfTo<EntitiesContainer>().FromNew().AsSingle();
            Container.Bind<LevelBounds.LevelBounds>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<EntityOutOfBoundsController>().FromNew().AsSingle();
            Container.Bind<IPositionWrapper>().To<InvertedClampedEntityPositionWrapper>().FromNew().AsSingle();
        }
    }
}