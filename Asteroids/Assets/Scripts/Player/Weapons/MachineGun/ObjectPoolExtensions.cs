using UnityEngine.Pool;

namespace Player.Weapons.MachineGun
{
    public static class ObjectPoolExtensions
    {
        public static void PreWarm<T>(this ObjectPool<T> pool, int count) where T : class
        {
            var items = new T[count];
            for (int i = 0; i < count; i++)
                items[i] = pool.Get();
            foreach (var item in items)
                pool.Release(item);
        }
    }
}