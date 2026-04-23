using _Project.Scripts.UI;
using _Project.Scripts.UI.Windows;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MainMenuWindow>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuModel>().AsSingle();
        }
    }
}