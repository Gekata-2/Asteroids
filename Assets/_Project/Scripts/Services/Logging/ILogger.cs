using System;

namespace _Project.Scripts.Services.Logging
{
    public interface ILogger
    {
        void LogWarning(string text);
        void LogException(Exception exception);
        void LogSave(string text);
        void LogAnalytics(string text);
        void LogAds(string text);
    }
}