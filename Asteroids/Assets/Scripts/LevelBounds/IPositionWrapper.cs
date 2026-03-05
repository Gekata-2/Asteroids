using Entities;

namespace LevelBounds
{
    interface IPositionWrapper
    {
        void WrapEntityPosition(Entity entity, LevelBounds bounds);
    }
}