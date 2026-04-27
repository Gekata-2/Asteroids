using _Project.Scripts.Entities;
using _Project.Scripts.Entities.Asteroids;
using _Project.Scripts.Level.BoundsHandling;
using _Project.Scripts.Level.GameSession;
using _Project.Scripts.Services;
using _Project.Scripts.Services.BeginGame;
using _Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private LevelAssetsConfig _levelAssetsConfig;

        public override void InstallBindings()
        {
            Container.Bind<LevelAssetsConfig>().FromScriptableObject(_levelAssetsConfig).AsSingle();

            Container.BindInterfacesAndSelfTo<EntitiesContainer>().FromNew().AsSingle();
            Container.Bind<LevelBounds>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<PausePresenter>().AsSingle();
            Container.Bind<GameSessionData>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameOverModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameOverPresenter>().AsSingle();

            Container.Bind<EntitiesController>().FromComponentsInHierarchy().AsSingle();
            Container.BindInterfacesTo<AsteroidsController>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<BeginGameModel>().AsSingle();
        }
    }
}