using _Project.Scripts.Entities.Asteroids;
using _Project.Scripts.Entities.UFO;

namespace _Project.Scripts.Entities
{
    public interface IDamageVisitor
    {
        void Visit(Player.Player player);
        void Visit(Asteroid asteroid);
        void Visit(Ufo ufo);
    }
}