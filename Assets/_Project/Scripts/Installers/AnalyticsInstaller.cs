using _Project.Scripts.Meta.Analytics;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class AnalyticsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<FirebaseAnalyticsService>().AsSingle();
        }
    }
}