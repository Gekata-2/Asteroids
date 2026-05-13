using System;
using _Project.Scripts.Services.Network;
using Zenject;

namespace _Project.Scripts.Network
{
    public class NetworkPresenter : IInitializable, IDisposable
    {
        private readonly NetworkWindow _view;
        private readonly INetworkConnectionService _connectionService;

        public NetworkPresenter(NetworkWindow view, INetworkConnectionService connectionService)
        {
            _connectionService = connectionService;
            _view = view;
        }

        public void Initialize()
        {
            _connectionService.ConnectionStatusChanged += OnConnectionStatusChanged;
        }

        private void OnConnectionStatusChanged(bool isConnected)
        {
            _view.SetIsConnected(isConnected);
        }

        public void Dispose()
        {
            _connectionService.ConnectionStatusChanged -= OnConnectionStatusChanged;
        }
    }
}