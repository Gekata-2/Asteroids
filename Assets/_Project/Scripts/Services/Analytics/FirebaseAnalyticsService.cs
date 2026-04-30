using System;
using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Analytics;
using UnityEngine;
using ILogger = _Project.Scripts.Services.Logging.ILogger;

namespace _Project.Scripts.Services.Analytics
{
    public class FirebaseAnalyticsService : IAnalytics
    {
        private readonly ILogger _logger;

        public FirebaseAnalyticsService(ILogger logger = null)
        {
            _logger = logger;
        }

        public async UniTask Initialize()
        {
            try
            {
                DependencyStatus status = await FirebaseApp.CheckAndFixDependenciesAsync().AsUniTask();
                if (status != DependencyStatus.Available)
                    Debug.LogException(new Exception($"Couldn't resolve all Firebase dependencies: {status}"));

                Debug.Log("Firebase initialized successfully!!!");
                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public void LogGameStarted()
        {
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart);
            _logger?.LogAnalytics($"Analytics: {FirebaseAnalytics.EventLevelStart}");
        }

        public void LogGameOver(GameOverAnalyticsData data)
        {
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelEnd,
                new Parameter("shots_fired", data.ShotsFired),
                new Parameter("laser_used", data.LaserUsed),
                new Parameter("asteroids_destroyed", data.AsteroidsDestroyed),
                new Parameter("ufo_destroyed", data.UfoDestroyed)
            );
            _logger?.LogAnalytics($"Analytics: {FirebaseAnalytics.EventLevelEnd} : {data}");
        }

        public void LogLaserUsed()
        {
            FirebaseAnalytics.LogEvent("laser_used");
            _logger?.LogAnalytics($"Analytics: laser_used");
        }
    }
}