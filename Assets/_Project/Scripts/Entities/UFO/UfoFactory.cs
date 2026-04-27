using _Project.Scripts.EnemyAI.StateMachine;
using _Project.Scripts.EnemyAI.StateMachine.States;
using _Project.Scripts.Player;
using _Project.Scripts.Services.AssetsProviding;
using _Project.Scripts.Services.BeginGame;
using _Project.Scripts.Services.RemoteConfigs;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Entities.UFO
{
    public class UfoFactory : IAssetFetcher, IConfigFetcher
    {
        private const string CONTAINER_NAME = "UFO container";

        private readonly DiContainer _di;
        private readonly GameObject _container;
        private readonly AssetsNames _assetsNames;
        private readonly IAssetProvider _assetProvider;

        private UfoConfig _config;
        private Object _prefab;

        public UfoFactory(DiContainer di, AssetsNames assetsNames, IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _assetsNames = assetsNames;
            _di = di;

            _container = new GameObject(CONTAINER_NAME);
        }

        public void FetchAssets()
        {
            _assetProvider.TryGetAsset(_assetsNames.GetName(Asset.Ufo), out _prefab);
        }

        public void FetchConfig(IConfigsProvider configsProvider)
        {
            _config = configsProvider.GetValue<UfoConfig>(ConfigsNames.Ufo);
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