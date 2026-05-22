using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Entities.Asteroids.Configs;
using _Project.Scripts.Entities.Asteroids.Pools;
using _Project.Scripts.Services.RemoteConfigs;
using Zenject;

namespace _Project.Scripts.Entities.Asteroids
{
    public class AsteroidsConfigsRegistry : IInitializable
    {
        private readonly Dictionary<AsteroidType, string> _configNames = new()
        {
            { AsteroidType.Small, ConfigsNames.AsteroidSmall },
            { AsteroidType.Big, ConfigsNames.AsteroidBig },
        };

        private readonly Dictionary<string, AsteroidConfig> _configs = new()
        {
            { ConfigsNames.AsteroidSmall, null },
            { ConfigsNames.AsteroidBig, null },
        };

        private readonly IConfigsProvider _configsProvider;
        private SplitConfigs _splitConfigs;
      
        public List<AsteroidsSplitConfig> Chain => _splitConfigs.Chain;
        
        public AsteroidsConfigsRegistry(IConfigsProvider configsProvider)
        {
            _configsProvider = configsProvider;
        }

        public void Initialize()
        {
            foreach (string key in _configNames.Values)
            {
                AsteroidConfig config = _configsProvider.GetValue<AsteroidConfig>(key);
                _configs[key] = config;
            }

            _splitConfigs = _configsProvider.GetValue<SplitConfigs>(ConfigsNames.AsteroidsChain);
        }

        public AsteroidConfig GetConfig(AsteroidType asteroidType)
            => GetConfigByName(_configNames[asteroidType]);

        private AsteroidConfig GetConfigByName(string asteroidType)
            => _configs.TryGetValue(asteroidType, out AsteroidConfig config) ? config : null;

        public AsteroidConfig GetFirstConfig()
            => GetConfig(Chain.First().AsteroidType);
    }
}