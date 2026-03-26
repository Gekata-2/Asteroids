using System;

namespace _Project.Scripts.Player.Weapons.Laser
{
    public class LaserModel
    {
        public event Action<int> ChargesCountChanged;
        public event Action<float> CooldownTimeLeftChanged;

        private readonly LaserConfig _config;
        private readonly LaserCharges _charges;

        public bool IsOnCooldown { get; private set; }
        private float _cooldownTimeLeft;
        
        public bool IsNoChargesLeft => _charges.IsZero();
        public bool IsChargesFull => _charges.IsFull();
        public float Duration => _config.Duration;
        public float Cooldown => _config.Cooldown;
        public float Lenght => _config.Lenght;
        public int Charges => _charges.Current;

        public LaserModel(PlayerWeaponsConfig playerWeaponsConfig)
        {
            _config = playerWeaponsConfig.Laser;
            _charges = new LaserCharges(_config.Charges);
        }

        public void SetCooldownTimeLeft(float time)
        {
            _cooldownTimeLeft = time;
            CooldownTimeLeftChanged?.Invoke(_cooldownTimeLeft);
        }

        public void SetIsOnCooldown(bool isOnCooldown)
        {
            IsOnCooldown = isOnCooldown;
        }

        public void UseCharge()
        {
            _charges.UseCharge();
            ChargesCountChanged?.Invoke(_charges.Current);
        }

        public void RestoreCharge()
        {
            _charges.RestoreCharge();
            ChargesCountChanged?.Invoke(_charges.Current);
        }
    }
}