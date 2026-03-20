namespace _Project.Scripts.EnemyAI.StateMachine
{
    public interface IState
    {
        void FixedUpdate();
        void OnEnter();
        
        string GetName() => GetType().Name;
    }
}