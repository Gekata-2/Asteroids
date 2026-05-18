using System;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Services.Network
{
    public interface INetworkConnectionService
    {
        event Action<bool> ConnectionStatusChanged;
        bool IsOnline { get; }
        UniTask Connect();
    }
}