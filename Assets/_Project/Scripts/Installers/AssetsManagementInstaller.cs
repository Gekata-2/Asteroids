using _Project.Scripts.Services.AssetsManagement;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class AssetsManagementInstaller : MonoInstaller
    {
        [SerializeField] private AssetBundleConfig assetBundleConfig;

        public override void InstallBindings()
        {
            Container.Bind<AssetBundleConfig>().FromScriptableObject(assetBundleConfig).AsSingle();
            Container.BindInterfacesTo<AddressablesProvider>().AsSingle();
        }
    }
}