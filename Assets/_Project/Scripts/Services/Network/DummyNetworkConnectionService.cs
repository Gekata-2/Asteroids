using System;
using _Project.Scripts.Player;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.Services.Network
{
    public class DummyNetworkConnectionService : INetworkConnectionService, IInitializable, IDisposable
    {
        public event Action<bool> ConnectionStatusChanged;
        private readonly IInput _input;
        private bool _isOnline;

        public bool IsOnline
        {
            get => _isOnline;
            private set
            {
                _isOnline = value;
                ConnectionStatusChanged?.Invoke(_isOnline);
            }
        }

        public DummyNetworkConnectionService(IInput input)
        {
            _input = input;
        }

        public UniTask Connect()
        {
            IsOnline = true;
            return UniTask.CompletedTask;
        }

        public void Initialize()
        {
            _input.ConnectionSwitchPerformed += OnConnectionSwitchPerformed;
        }

        private void OnConnectionSwitchPerformed()
            => IsOnline = !IsOnline;

        public void Dispose()
        {
            _input.ConnectionSwitchPerformed -= OnConnectionSwitchPerformed;
        }
    }
}