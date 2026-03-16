using UnityEngine;

namespace _Project.Scripts.Entities.Spawner
{
    public class RectangleSideSpawnPositionPicker : ISpawnPositionPicker
    {
        private readonly Vector2 _size;
        private readonly Color _gizmosColor;
        
        public RectangleSideSpawnPositionPicker(Vector2 size, Color gizmosColor)
        {
            _size = size;
            _gizmosColor = gizmosColor;
        }

        public Vector2 GetNextPosition()
        {
            Vector2 pos = Vector2.zero;
            int side = Random.Range(1, 5);
            pos = side switch
            {
                1 => new Vector2(Random.Range(0f, 1f), 0f),
                2 => new Vector2(1, Random.Range(0f, 1f)),
                3 => new Vector2(Random.Range(0f, 1f), 1f),
                4 => new Vector2(0, Random.Range(0f, 1f)),
                _ => pos
            };

            pos -= Vector2.one / 2f;
            pos *= _size;
            return pos;
        }

        public void DrawGizmos()
        {
            Gizmos.color = _gizmosColor;
            Gizmos.DrawWireCube(Vector3.zero, _size);
        }
    }
}