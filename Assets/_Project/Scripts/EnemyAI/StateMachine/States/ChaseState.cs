using _Project.Scripts.Entities.UFO;
using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts.EnemyAI.StateMachine.States
{
    public class ChaseState : BaseState
    {
        private readonly EnemyTarget _player;
        
        public ChaseState(EnemyTarget player, Ufo ufo) : base(ufo)
        {
            _player = player;
        }

        public override void OnEnter()
        {
            Ufo.SetRotation(GetDesiredRotation());
        }

        public override void FixedUpdate()
        {
            float desiredRotation = GetDesiredRotation();

            Ufo.SetRotation(Mathf.MoveTowardsAngle(Ufo.Rotation, desiredRotation,
                Ufo.SteeringSpeed * Time.fixedDeltaTime));
            Ufo.SetVelocity(Ufo.Up * Ufo.Speed);
        }

        private float GetDesiredRotation()
        {
            Vector2 direction = (_player.Position - Ufo.Position).normalized;

            float desiredRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (desiredRotation < 0f)
                desiredRotation = 360f - Mathf.Abs(desiredRotation);
            desiredRotation -= 90f;
            return desiredRotation;
        }
    }
}