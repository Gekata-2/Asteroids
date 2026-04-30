using System.Collections.Generic;

namespace _Project.Scripts.Services.AssetsProviding
{
    public class AssetsNames
    {
        private readonly Dictionary<Asset, string> _assetsNames = new()
        {
            { Asset.AsteroidBig, "asteroid_big" },
            { Asset.AsteroidSmall, "asteroid_small" },
            { Asset.Ufo, "ufo" },
            { Asset.Player, "player" },
            { Asset.PauseUI, "pause_ui" },
            { Asset.GameOverUI, "game_over_ui" },
            { Asset.PlayerStateUI, "player_state_ui" },
            { Asset.LaserUI, "laser_ui" },
            { Asset.ScoreUI, "score_ui" },
        };

        public string GetName(Asset asset)
            => _assetsNames[asset];
    }
}