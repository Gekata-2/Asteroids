using _Project.Scripts.Network;
using _Project.Scripts.Services.Network;
using _Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class NetworkInstaller : MonoInstaller
    {
        [SerializeField] private NetworkWindow _networkWindowPrefab;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<NetworkWindow>().FromComponentInNewPrefab(_networkWindowPrefab)
                .AsSingle();
            Container.BindInterfacesAndSelfTo<NetworkPresenter>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<DummyNetworkConnectionService>().AsSingle();
        }
    }
}