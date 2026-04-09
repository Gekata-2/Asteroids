using _Project.Scripts.DataPersistence;
using _Project.Scripts.Services.SceneManagement;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SceneLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerPrefsSaveLoadService>().AsSingle().WithArguments(true);
        }
    }
}