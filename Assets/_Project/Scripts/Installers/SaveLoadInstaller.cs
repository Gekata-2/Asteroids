using _Project.Scripts.Services.DataPersistence;
using _Project.Scripts.Services.DataPersistence.Cloud;
using _Project.Scripts.Services.DataPersistence.Local;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class SaveLoadInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ILocalSaveLoadService>().To<PlayerPrefsSaveService>().AsCached();
            Container.Bind<ICloudSaveLoadService>().To<UnityCloudSaveService>().AsCached();
            Container.Bind<SaveLoadService>().AsSingle();
        }
    }
}