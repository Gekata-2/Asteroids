using _Project.Scripts.Services.AssetsManagement;
using UnityEngine;

namespace _Project.Scripts.Vfx
{
    public struct VfxPoolData
    {
        public int PrewarmSize { get; }
        public int DefaultCapacity { get; }
        public int MaxSize { get; }
        public VFX VFX { get; }
        public Transform Container { get; }
        public Vector2 DefaultInactivePosition { get; }

        public VfxPoolData(int prewarmSize, int defaultCapacity, int maxSize,
            VFX vfx, Transform container,
            Vector2 defaultInactivePosition)
        {
            PrewarmSize = prewarmSize;
            DefaultCapacity = defaultCapacity;
            MaxSize = maxSize;
            VFX = vfx;
            Container = container;
            DefaultInactivePosition = defaultInactivePosition;
        }
    }
}