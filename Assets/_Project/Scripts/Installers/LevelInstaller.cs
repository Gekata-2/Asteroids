using _Project.Scripts.Entities;
using _Project.Scripts.Entities.Asteroids;
using _Project.Scripts.GameOver;
using _Project.Scripts.Level;
using _Project.Scripts.Level.BoundsHandling;
using _Project.Scripts.Services.BeginGame;
using _Project.Scripts.Services.Pause;
using _Project.Scripts.UI;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EntitiesContainer>().FromNew().AsSingle();
            Container.Bind<LevelBounds>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<PausePresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<PauseModel>().AsSingle();
            Container.Bind<GameSessionData>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameOverModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameOverPresenter>().AsSingle();

            Container.Bind<EntitiesController>().FromComponentsInHierarchy().AsSingle();
            Container.BindInterfacesTo<AsteroidsController>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<BeginGameModel>().AsSingle();
        }
    }
}