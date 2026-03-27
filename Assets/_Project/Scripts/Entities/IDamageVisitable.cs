namespace _Project.Scripts.Entities
{
    interface IDamageVisitable
    {
        void Accept(IDamageVisitor visitor);
    }
}