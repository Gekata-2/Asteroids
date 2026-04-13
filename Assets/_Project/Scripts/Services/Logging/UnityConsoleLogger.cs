using UnityEngine;

namespace _Project.Scripts.Services.Logging
{
    public class UnityConsoleLogger : ILogger
    {
        private readonly LogModule _logConfig;

        public UnityConsoleLogger(LogModule config = LogModule.None)
        {
            _logConfig = config;
            
            if (_logConfig != LogModule.None) 
                Debug.Log($"Enabled Unity console logging for: {_logConfig}");
        }

        public void LogSave(string text)
        {
            if (_logConfig.HasFlag(LogModule.Saving))
                Debug.Log(text);
        }

        public void LogAnalytics(string text)
        {
            if (_logConfig.HasFlag(LogModule.Analytics))
                Debug.Log(text);
        }
    }
}