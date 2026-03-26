using UnityEngine;

namespace _Project.Scripts.Player.Weapons
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Create Player Weapons Config", fileName = "Player Weapons Config",
        order = 0)]
    public class PlayerWeaponsConfig : ScriptableObject
    {
        [field: SerializeField] public MachineGunConfig MachineGun { get; private set; }
        [field: SerializeField] public LaserConfig Laser { get; private set; }
    }
}