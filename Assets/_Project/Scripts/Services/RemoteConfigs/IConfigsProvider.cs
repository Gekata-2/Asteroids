using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Services.RemoteConfigs
{
    public interface IConfigsProvider
    {
        UniTask FetchData();
        UniTask ActivateData();
        T GetValue<T>(string key);
    }
}