using UnityEngine;

namespace _Project.Scripts.Player.Weapons.MachineGun
{
    public struct BulletData
    {
        public float Speed { get; }

        public float LifeTime { get; }

        public Vector2 MoveDirection { get; }

        public BulletData(float speed, Vector2 moveDirection, float lifeTime)
        {
            MoveDirection = moveDirection;
            LifeTime = lifeTime;
            Speed = speed;
        }
    }
}