using System.Collections.Generic;
using _Project.Scripts.Sfx;
using _Project.Scripts.Vfx;

namespace _Project.Scripts.Services.AssetsManagement
{
    public static class AssetsNames
    {
        private static readonly Dictionary<Asset, string> _assets = new()
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

        private static readonly Dictionary<VFX, string> _vfx = new()
        {
            { VFX.UfoDestroyed, "ufo_destroyed_vfx" },
            { VFX.AsteroidDestroyed, "asteroid_destroyed_vfx" },
            { VFX.MachineGunShoot, "machine_gun_shoot_vfx" },
        };

        private static readonly Dictionary<SFX, string> _sfx = new()
        {
            { SFX.EnemyDestroyed, "asteroid_destroyed_sfx" },
            { SFX.MachineGunShoot, "machine_gun_shoot_sfx" },
            { SFX.MainMenuOst, "main_menu_ost" },
            { SFX.LevelOst, "level_ost" },
        };

        public static string GetName(Asset asset)
            => _assets[asset];

        public static string GetName(VFX vfx)
            => _vfx[vfx];

        public static string GetName(SFX sfx)
            => _sfx[sfx];
    }
}