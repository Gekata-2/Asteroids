using UnityEngine;

namespace _Project.Scripts.Entities.Spawner
{
    interface ISpawnPositionPicker
    {
        Vector2 GetNextPosition();
        void DrawGizmos();
    }
}