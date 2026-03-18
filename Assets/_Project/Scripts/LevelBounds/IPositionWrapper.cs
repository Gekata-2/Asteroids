using _Project.Scripts.Entities;

namespace _Project.Scripts.LevelBounds
{
    interface IPositionWrapper
    {
        void WrapEntityPosition(Entity entity, LevelBounds bounds);
    }
}