namespace Entities
{
    public class Damage
    {
        public object Source { get; }

        public Damage(object source)
        {
            Source = source;
        }
    }
}