using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Meta.Analytics
{
    public interface IAnalyticsService
    {
        UniTask Initialize();
        void LogGameStarted();
        void LogGameOver(GameOverAnalyticsData data);
        void LogLaserUsed();
    }
}