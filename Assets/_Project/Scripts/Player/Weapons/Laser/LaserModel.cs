using System;
using _Project.Scripts.Meta.Analytics;
using _Project.Scripts.Player.Weapons.Configs;
using _Project.Scripts.Services.RemoteConfigs;
using Zenject;

namespace _Project.Scripts.Player.Weapons.Laser
{
    public class LaserModel : IInitializable
    {
        private readonly IAnalytics _analytics;
        public event Action<int> ChargesCountChanged;
        public event Action<float> CooldownTimeLeftChanged;

        private LaserConfig _config;
        private LaserCharges _charges;
        private readonly IConfigsProvider _configsProvider;


        public float CooldownTimeLeft { get; private set; }

        public int UsedCount { get; private set; }
        public bool IsOnCooldown { get; private set; }

        public bool IsNoChargesLeft => _charges.IsZero();
        public bool IsChargesFull => _charges.IsFull();
        public float Duration => _config.Duration;
        public float Cooldown => _config.Cooldown;
        public float Lenght => _config.Lenght;
        public int Charges => _charges.Current;

        public LaserModel(IAnalytics analytics, IConfigsProvider configsProvider)
        {
            _analytics = analytics;
            _configsProvider = configsProvider;
        }

        public void Initialize()
        {
            PlayerWeaponsConfig weaponsConfig =
                _configsProvider.GetValue<PlayerWeaponsConfig>(ConfigsNames.PlayerWeapons);
            _config = weaponsConfig.Laser;
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

        public void RestoreAllCharges()
        {
            _charges.RestoreCharges();
            ChargesCountChanged?.Invoke(_charges.Current);
            SetIsOnCooldown(false);
            SetCooldownTimeLeft(0f);
        }
    }
}