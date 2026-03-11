using UnityEngine;
using Zenject;

namespace Entities.UFO
{
    class RandomOuterSquarePositionPicker
    {
        private float _sideSize;

        public RandomOuterSquarePositionPicker(float sideSize)
        {
            _sideSize = sideSize;
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
            pos *= _sideSize;
            return pos;
        }

        public void DrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(Vector3.zero, _sideSize * Vector3.one);
        }
    }
}