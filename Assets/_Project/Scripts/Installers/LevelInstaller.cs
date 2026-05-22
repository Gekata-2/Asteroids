using _Project.Scripts.Entities;
using _Project.Scripts.Entities.Asteroids;
using _Project.Scripts.GameOver;
using _Project.Scripts.Level;
using _Project.Scripts.Level.BoundsHandling;
using _Project.Scripts.Services.BeginGame;
using _Project.Scripts.Services.Pause;
using _Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EntitiesContainer>().FromNew().AsSingle();
            Container.Bind<LevelBounds>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesTo<PausePresenter>().AsSingle();
            Container.Bind<PauseModel>().AsSingle();
            Container.Bind<GameSessionData>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameOverModel>().AsSingle();
            Container.BindInterfacesTo<GameOverPresenter>().AsSingle();

            Container.Bind<EntitiesController>().FromComponentsInHierarchy().AsSingle();
            Container.BindInterfacesTo<AsteroidsController>().FromComponentInHierarchy().AsSingle();

            Container.Bind<BeginGameModel>().AsSingle();
            
            Container.BindFactory<Object, PauseWindow, PauseWindowPrefabFactory>().FromFactory<PrefabFactory<PauseWindow>>();
            Container.Bind<PauseWindowFactory>().AsSingle();
            Container.BindFactory<Object, GameOverWindow, GameOverWindowPrefabFactory>().FromFactory<PrefabFactory<GameOverWindow>>();
            Container.Bind<GameOverWindowFactory>().AsSingle();
        }
    }
}