using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Services.Analytics
{
    public interface IAnalytics
    {
        UniTask Initialize();
        void LogGameStarted();
        void LogGameOver(GameOverAnalyticsData data);
        void LogLaserUsed();
    }
}