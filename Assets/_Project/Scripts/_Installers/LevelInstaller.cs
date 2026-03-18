using _Project.Scripts.Entities;
using _Project.Scripts.Level.BoundsHandling;
using _Project.Scripts.Level.GameSession;
using _Project.Scripts.Player;
using _Project.Scripts.Services.Awards;
using _Project.Scripts.UI.Windows;
using UnityEngine;
using Zenject;

namespace _Project.Scripts._Installers
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private GameSettings gameSettings;
        [SerializeField] private GameOverWindow gameOverWindowPrefab;
        [SerializeField] private PauseWindow pauseWindowPrefab;
        
        public override void InstallBindings()
        {
            Container.Bind<GameSettings>().FromScriptableObject(gameSettings).AsSingle();
            
            Container.BindInterfacesAndSelfTo<EntitiesContainer>().FromNew().AsSingle();
            Container.Bind<LevelBounds>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<EntityOutOfBoundsController>().FromNew().AsSingle();
            Container.Bind<IPositionWrapper>().To<InvertedClampedEntityPositionWrapper>().FromNew().AsSingle();

            Container.Bind<GameOverWindow>().FromComponentInNewPrefab(gameOverWindowPrefab).AsSingle().NonLazy();
            Container.Bind<PauseWindow>().FromComponentInNewPrefab(pauseWindowPrefab).AsSingle().NonLazy();

            Container.Bind<GameSessionModel>().To<GameSessionState>().FromNew().AsSingle();
        }
    }
}