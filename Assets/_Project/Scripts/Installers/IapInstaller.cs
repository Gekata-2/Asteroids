using _Project.Scripts.Meta.IAP;
using _Project.Scripts.Services.Authorization;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class IapInstaller : MonoInstaller
    {
        [SerializeField] private IapConfig _iapConfig;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AnonymousAuthorizationService>().AsSingle();
            Container.BindInterfacesAndSelfTo<UnityIAPService>().AsSingle();
            Container.BindInterfacesAndSelfTo<IAPModel>().AsSingle();
            Container.Bind<IapConfig>().FromScriptableObject(_iapConfig).AsSingle();
        }
    }
}