using Player;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerState : PlayerModel
    {
        private IPlayerMovement _player;

        public override void SetPlayer(IPlayerMovement player)
            => _player = player;

        public override Vector2 Position => _player?.Position ?? default;
        public override float Speed => _player?.Speed ?? default;
        public override float Angle => _player?.Rotation ?? default;
    }
}