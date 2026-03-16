using Entities.UFO;
using UnityEngine;

namespace EnemyAI.StateMachine.States
{
    public class ChaseState : BaseState
    {
        private readonly IEnemyTargetable _player;
        private UfoData _data;

        public ChaseState(IEnemyTargetable player, UFO enemy) : base(enemy, "Chase")
        {
            _player = player;
        }

        public override void OnEnter()
        {
            _data = _enemy.Data as UfoData;
            SetRotation(GetDesiredRotation());
        }

        public override void FixedUpdate()
        {
            float desiredRotation = GetDesiredRotation();

            SetRotation(Mathf.MoveTowardsAngle(_enemy.Rotation, desiredRotation,
                _data.Movement.SteeringSpeed * Time.fixedDeltaTime));
            _enemy.Rb.linearVelocity = _enemy.transform.up * _data.Movement.Speed;
        }

        private void SetRotation(float rotation)
        {
            _enemy.Rotation = rotation;
            _enemy.Rb.SetRotation(Quaternion.Euler(0, 0, _enemy.Rotation));
        }

        private float GetDesiredRotation()
        {
            Vector2 direction = (_player.Position - _enemy.Rb.position).normalized;

            float desiredRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (desiredRotation < 0f)
                desiredRotation = 360f - Mathf.Abs(desiredRotation);
            desiredRotation -= 90f;
            return desiredRotation;
        }
    }
}