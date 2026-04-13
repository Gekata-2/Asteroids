namespace _Project.Scripts.Services.Logging
{
    public interface ILogger
    {
        void LogSave(string text);
        void LogAnalytics(string text);
    }
}