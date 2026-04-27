namespace _Project.Scripts.Services.RemoteConfigs
{
    public interface IConfigFetcher
    {
        void FetchConfig(IConfigsProvider configsProvider);
    }
}