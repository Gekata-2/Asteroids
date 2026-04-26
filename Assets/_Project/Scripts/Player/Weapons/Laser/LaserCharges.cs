using UnityEngine;

namespace _Project.Scripts.Player.Weapons.Laser
{
    public class LaserCharges
    {
        public LaserCharges(int max)
        {
            Max = max;
            Current = max;
        }

        private int Max { get; }
        public int Current { get; private set; }

        public void UseCharge()
        {
            if (IsZero())
                return;

            Current = Mathf.Clamp(Current - 1, 0, Current);
        }

        public void RestoreCharge()
        {
            if (IsFull())
                return;
            
            Current = Mathf.Clamp(Current + 1, Current, Max + 1);
        }

        public void RestoreCharges() 
            => Current = Max;

        public bool IsZero()
            => Current == 0;

        public bool IsFull()
            => Current == Max;
    }
}