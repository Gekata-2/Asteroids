using System;
using _Project.Scripts.Analytics;

namespace _Project.Scripts.Player.Weapons.Laser
{
    public class LaserModel
    {
        private readonly IAnalytics _analytics;
        public event Action<int> ChargesCountChanged;
        public event Action<float> CooldownTimeLeftChanged;

        private readonly LaserConfig _config;
        private readonly LaserCharges _charges;

        public float CooldownTimeLeft { get; private set; }
        
        public int UsedCount { get; private set; }
        public bool IsOnCooldown { get; private set; }
        
        public bool IsNoChargesLeft => _charges.IsZero();
        public bool IsChargesFull => _charges.IsFull();
        public float Duration => _config.Duration;
        public float Cooldown => _config.Cooldown;
        public float Lenght => _config.Lenght;
        public int Charges => _charges.Current;

        public LaserModel(PlayerWeaponsConfig playerWeaponsConfig, IAnalytics analytics)
        {
            _analytics = analytics;
            _config = playerWeaponsConfig.Laser;
            _charges = new LaserCharges(_config.Charges);
        }

        public void SetCooldownTimeLeft(float time)
        {
            CooldownTimeLeft = time;
            CooldownTimeLeftChanged?.Invoke(CooldownTimeLeft);
        }

        public void SetIsOnCooldown(bool isOnCooldown)
        {
            IsOnCooldown = isOnCooldown;
        }

        public void UseCharge()
        {
            ++UsedCount;
            _charges.UseCharge();
            _analytics.LogLaserUsed();
            ChargesCountChanged?.Invoke(_charges.Current);
        }

        public void RestoreCharge()
        {
            _charges.RestoreCharge();
            ChargesCountChanged?.Invoke(_charges.Current);
        }
    }
}