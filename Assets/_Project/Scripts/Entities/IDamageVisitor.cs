using _Project.Scripts.Entities.Asteroids;
using _Project.Scripts.Entities.UFO;
using _Project.Scripts.Player;

namespace _Project.Scripts.Entities
{
    public interface IDamageVisitor
    {
        void Visit(PlayerHealth playerHealth);
        void Visit(Asteroid asteroid);
        void Visit(Ufo ufo);
    }
}