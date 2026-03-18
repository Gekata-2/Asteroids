namespace _Project.Scripts.Entities
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