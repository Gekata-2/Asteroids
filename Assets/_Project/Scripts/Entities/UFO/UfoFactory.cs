using _Project.Scripts.EnemyAI.StateMachine;
using _Project.Scripts.EnemyAI.StateMachine.States;
using _Project.Scripts.Player;
using _Project.Scripts.Services.AssetsManagement;
using _Project.Scripts.Services.RemoteConfigs;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Entities.UFO
{
    public class UfoFactory : IAssetFetcher, IInitializable
    {
        private const string CONTAINER_NAME = "UFO container";

        private readonly DiContainer _di;
        private readonly GameObject _container;
        private readonly IAssetProvider _assetProvider;
        private readonly IConfigsProvider _configsProvider;

        private UfoConfig _config;
        private Object _prefab;

        public UfoFactory(DiContainer di, IAssetProvider assetProvider, IConfigsProvider configsProvider)
        {
            _assetProvider = assetProvider;
            _configsProvider = configsProvider;
            _di = di;

            _container = new GameObject(CONTAINER_NAME);
        }

        public void Initialize()
        {
            _config = _configsProvider.GetValue<UfoConfig>(ConfigsNames.Ufo);
        }

        public void FetchAssets()
        {
            _assetProvider.TryGetAsset(AssetsNames.GetName(Asset.Ufo), out _prefab);
        }

        public Ufo Create(Vector3 position, EnemyTarget target)
        {
            Ufo ufo = _di.InstantiatePrefabForComponent<Ufo>(
                _prefab,
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