using _Project.Scripts.Services.RemoteConfigs;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class RemoteConfigsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<FirebaseRemoteConfigsProvider>().AsSingle();
        }
    }
}