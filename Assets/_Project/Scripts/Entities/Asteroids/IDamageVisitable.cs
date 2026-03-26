namespace _Project.Scripts.Entities.Asteroids
{
    interface IDamageVisitable
    {
        void Accept(IDamageVisitor visitor);
    }
}