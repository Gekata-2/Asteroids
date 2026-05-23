using _Project.Scripts.MainMenu;
using _Project.Scripts.Meta.IAP;
using _Project.Scripts.Meta.IAP.UI;
using _Project.Scripts.Services.DataPersistence.SaveResolving;
using _Project.Scripts.UI;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MainMenuWindow>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesTo<MainMenuPresenter>().AsSingle();
            Container.Bind<MainMenuModel>().AsSingle();

            Container.Bind<IAPWindow>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesTo<IAPPresenter>().AsSingle();

            Container.BindInterfacesAndSelfTo<SaveResolvePresenter>().AsSingle();
            Container.Bind<SaveResolveWindow>().FromComponentInHierarchy().AsSingle();
        }
    }
}