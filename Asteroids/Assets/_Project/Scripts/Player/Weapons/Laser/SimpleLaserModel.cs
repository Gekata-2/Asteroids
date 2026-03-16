using UI.Laser;

namespace Player.Weapons.Laser
{
    public class SimpleLaserModel : LaserModel
    {
        public SimpleLaserModel(LaserView view) : base(view)
        {
        }

        public override void SetConfig(PlayerWeaponsConfig.LaserConfig config)
        {
            Config = config;
            Charges = new LaserCharges(Config.Charges);
            View.SetChargesCount(Charges.Current);
        }

        public override void SetCooldownTimeLeft(float time)
        {
            CooldownTimeLeft = time;
            View.SetProgress(CooldownTimeLeft, Cooldown);
        }

        public override void SetIsOnCooldown(bool isOnCooldown)
        {
            IsOnCooldown = isOnCooldown;
        }

        public override void UseCharge()
        {
            Charges.UseCharge();
            View.SetChargesCount(Charges.Current);
        }

        public override void RestoreCharge()
        {
            Charges.RestoreCharge();
            View.SetChargesCount(Charges.Current);
        }

        public override void ResetCharges()
        {
            Charges.ResetCharges();
            View.SetChargesCount(Charges.Current);
        }
    }
}