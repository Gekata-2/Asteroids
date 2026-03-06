using UnityEngine;

namespace Player
{
    class LaserCharges
    {
        public LaserCharges(int max)
        {
            Max = max;
            Current = max;
        }

        public int Max { get; private set; }
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

        public bool IsZero()
            => Current == 0;

        public bool IsFull()
            => Current == Max;

        public void ResetCharges()
            => Current = Max;
    }
}