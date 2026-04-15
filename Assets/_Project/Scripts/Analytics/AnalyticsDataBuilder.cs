using _Project.Scripts.Level.GameSession;
using _Project.Scripts.Player.Weapons.Laser;
using _Project.Scripts.Player.Weapons.MachineGun;

namespace _Project.Scripts.Analytics
{
    public class AnalyticsDataBuilder
    {
        private readonly GameSessionData _sessionData;
        private readonly LaserModel _laserModel;
        private readonly MachineGunModel _machineGunModel;

        public AnalyticsDataBuilder(GameSessionData sessionData, LaserModel laserModel, MachineGunModel machineGunModel)
        {
            _sessionData = sessionData;
            _laserModel = laserModel;
            _machineGunModel = machineGunModel;
        }

        public GameOverAnalyticsData CreateGameOverData() => new(_machineGunModel.ShotsFired,
            _laserModel.UsedCount, _sessionData.AsteroidsDestroyed, _sessionData.UfoDestroyed);
    }
}