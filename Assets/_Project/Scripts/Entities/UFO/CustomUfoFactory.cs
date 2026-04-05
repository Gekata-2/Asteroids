using _Project.Scripts.EnemyAI.StateMachine;
using _Project.Scripts.EnemyAI.StateMachine.States;
using _Project.Scripts.Entities.UFO.Configs;
using _Project.Scripts.Player;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Entities.UFO
{
    public class UfoFactory : PlaceholderFactory<Vector3, EnemyTarget, Ufo>
    {
    }

    public class CustomUfoFactory : IFactory<Vector3, EnemyTarget, Ufo>
    {
        private const string CONTAINER_NAME = "UFO container";

        private readonly DiContainer _di;
        private readonly UfoConfig _config;
        private readonly GameObject _container;

        public CustomUfoFactory(DiContainer di, UfoConfig config)
        {
            _config = config;
            _di = di;

            _container = new GameObject(CONTAINER_NAME);
        }

        public Ufo Create(Vector3 position, EnemyTarget target)
        {
            Ufo ufo = _di.InstantiatePrefabForComponent<Ufo>(
                _config.Prefab,
                position,
                Quaternion.identity,
                _container.transform);

            StateMachine stateMachine = CreateStateMachine(ufo, target);
            ufo.Initialize(_config);
            ufo.SetBehaviour(stateMachine);

            return ufo;
        }

        private StateMachine CreateStateMachine(Ufo ufo, EnemyTarget target)
        {
            StateMachine stateMachine = new StateMachine();
            IdleState idleState = new(ufo);
            ChaseState chaseState = new(target, ufo);
            DieState dieState = new(ufo);

            stateMachine.AddTransition(idleState, chaseState, new FuncPredicate(() => ufo.Initialized));
            stateMachine.AddAnyTransition(dieState,
                new FuncPredicate(() => ufo.HasBeenHitByBullet || ufo.HasBeenSweepedByLaser));
            stateMachine.SetState(idleState);

            return stateMachine;
        }
    }
}