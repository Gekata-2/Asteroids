using _Project.Scripts.Entities;
using UnityEngine;

namespace _Project.Scripts.Level.BoundsHandling
{
    class EntityPositionWrapper
    {
        public void WrapEntityPosition(Entity entity, LevelBounds levelBounds)
        {
            Bounds bounds = levelBounds.Bounds;
            float skinWidth = levelBounds.SkinWidth;

            Vector3 position = entity.transform.position;
            Vector3 newPosition = position;
            Vector3 invertedPosition = -position;

            if (invertedPosition.x > bounds.extents.x || invertedPosition.x < -bounds.extents.x)
            {
                newPosition.x = Mathf.Clamp(invertedPosition.x,
                    -bounds.extents.x + skinWidth, bounds.extents.x - skinWidth);
            }

            if (invertedPosition.y > bounds.extents.y || invertedPosition.y < -bounds.extents.y)
            {
                newPosition.y = Mathf.Clamp(invertedPosition.y,
                    -bounds.extents.y + skinWidth, bounds.extents.y - skinWidth);
            }
            
            entity.SetPosition(newPosition);
        }
    }
}