namespace Asteroids
{
    public class Damage
    {
        public Damage(object source)
        {
            Source = source;
        }

        public object Source { get; }
    }
}