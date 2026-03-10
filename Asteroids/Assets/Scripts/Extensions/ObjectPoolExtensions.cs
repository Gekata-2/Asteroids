using UnityEngine.Pool;

namespace Extensions
{
    public static class ObjectPoolExtensions
    {
        public static void PreWarm<T>(this ObjectPool<T> pool, int count) where T : class
        {
            T[] items = new T[count];
            for (int i = 0; i < count; i++)
                items[i] = pool.Get();
            foreach (var item in items)
                pool.Release(item);
        }
    }
}