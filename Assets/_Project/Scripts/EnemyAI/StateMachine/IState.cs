namespace _Project.Scripts.EnemyAI.StateMachine
{
    public interface IState
    {
        string GetName();
        void Update();
        void FixedUpdate();
        void OnEnter();
        void OnExit();
    }
}