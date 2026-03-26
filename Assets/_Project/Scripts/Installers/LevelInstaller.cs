using _Project.Scripts.Entities;
using _Project.Scripts.Level.BoundsHandling;
using _Project.Scripts.Level.GameSession;
using _Project.Scripts.Services.Awards;
using _Project.Scripts.UI;
using _Project.Scripts.UI.Windows;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private ScoreConfig scoreConfig;
        [SerializeField] private GameOverWindow gameOverWindowPrefab;
        [SerializeField] private PauseWindow pauseWindowPrefab;

        public override void InstallBindings()
        {
            Container.Bind<ScoreConfig>().FromScriptableObject(scoreConfig).AsSingle();

            Container.BindInterfacesAndSelfTo<EntitiesContainer>().FromNew().AsSingle();
            Container.Bind<LevelBounds>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<PausePresenter>().AsSingle();
            Container.Bind<PauseWindow>().FromComponentInNewPrefab(pauseWindowPrefab).AsSingle().NonLazy();
            Container.Bind<GameOverWindow>().FromComponentInNewPrefab(gameOverWindowPrefab).AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<WindowsInputHandler>().AsSingle();
            Container.Bind<GamePausedModel>().AsSingle();
            Container.Bind<GameSessionData>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameOverModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameOverPresenter>().AsSingle();
        }
    }
}