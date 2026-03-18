using _Project.Scripts.Entities.UFO;
using UnityEngine;

namespace _Project.Scripts.EnemyAI.StateMachine.States
{
    public class ChaseState : BaseState
    {
        private readonly IEnemyTargetable _player;
        private UfoData.MovementData _data;

        public ChaseState(IEnemyTargetable player, UFO enemy) : base(enemy, "Chase")
        {
            _player = player;
        }

        public override void OnEnter()
        {
            _data = (_enemy.Data as UfoData)?.Movement;
            _enemy.SetRotation(GetDesiredRotation());
        }

        public override void FixedUpdate()
        {
            float desiredRotation = GetDesiredRotation();

            _enemy.SetRotation(Mathf.MoveTowardsAngle(_enemy.Rotation, desiredRotation,
                _data.SteeringSpeed * Time.fixedDeltaTime));
            _enemy.SetVelocity(_enemy.Up * _data.Speed);
        }

        private float GetDesiredRotation()
        {
            Vector2 direction = (_player.Position - _enemy.Position).normalized;

            float desiredRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (desiredRotation < 0f)
                desiredRotation = 360f - Mathf.Abs(desiredRotation);
            desiredRotation -= 90f;
            return desiredRotation;
        }
    }
}