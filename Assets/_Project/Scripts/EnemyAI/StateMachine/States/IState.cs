namespace _Project.Scripts.EnemyAI.StateMachine.States
{
    public interface IState
    {
        void FixedUpdate();
        void OnEnter();
        
        string GetName() => GetType().Name;
    }
}