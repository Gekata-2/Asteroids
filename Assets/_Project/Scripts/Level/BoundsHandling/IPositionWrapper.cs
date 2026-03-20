using _Project.Scripts.Entities;

namespace _Project.Scripts.Level.BoundsHandling
{
   public interface IPositionWrapper
    {
        void WrapEntityPosition(Entity entity, LevelBounds bounds);
    }
}