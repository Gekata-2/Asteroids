using _Project.Scripts.Services;
using _Project.Scripts.Services.Logging;
using _Project.Scripts.Services.SceneManagement;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SceneLoader>().AsSingle();
            Container.Bind<ExitGameService>().FromNew().AsSingle();

            Container.BindInterfacesTo<UnityConsoleLogger>().AsSingle()
                .WithArguments(LogModule.All).NonLazy();
            
            Container.Bind<CursorService>().FromNew().AsSingle();
        }
    }
}