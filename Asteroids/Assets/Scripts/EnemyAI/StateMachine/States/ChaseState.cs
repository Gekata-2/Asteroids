using Entities.UFO;
using UnityEngine;

namespace EnemyAI.StateMachine.States
{
    public class ChaseState : BaseState
    {
        private readonly IEnemyTargetable _player;

        public ChaseState(IEnemyTargetable player, UFO enemy) : base(enemy, "Chase")
        {
            _player = player;
        }

        public override void FixedUpdate()
        {
            Vector2 direction = (_player.Position - _enemy.Rb.position).normalized;
            _enemy.Rb.linearVelocity = direction;
        }
    }
}