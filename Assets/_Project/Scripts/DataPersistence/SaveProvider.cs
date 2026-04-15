using _Project.Scripts.Level.GameSession;

namespace _Project.Scripts.DataPersistence
{
    public class SaveProvider
    {
        private readonly GameSessionData _sessionData;

        public SaveProvider(GameSessionData sessionData)
        {
            _sessionData = sessionData;
        }

        public SaveData CreateSave() 
            => new(_sessionData.Score, _sessionData.TimeElapsed);
    }
}