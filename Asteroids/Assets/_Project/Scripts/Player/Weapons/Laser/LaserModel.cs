using UI.Laser;

namespace Player.Weapons.Laser
{
    public abstract class LaserModel
    {
        protected readonly LaserView View;
        protected PlayerWeaponsConfig.LaserConfig Config;
        protected LaserCharges Charges;
        
        public bool IsOnCooldown { get; protected set; }
        protected float CooldownTimeLeft;
        
        protected LaserModel(LaserView view)
        {
            View = view;
        }

        public bool IsNoChargesLeft => Charges.IsZero();
        public bool IsChargesFull => Charges.IsFull();
        public float Duration => Config.Duration;
        public float Cooldown => Config.Cooldown;
        public float Lenght => Config.Lenght;

        public abstract void SetConfig(PlayerWeaponsConfig.LaserConfig config);
        public abstract void SetCooldownTimeLeft(float time);
        public abstract void SetIsOnCooldown(bool isOnCooldown);
        public abstract void UseCharge();
        public abstract void RestoreCharge();
        public abstract void ResetCharges();
    }
}