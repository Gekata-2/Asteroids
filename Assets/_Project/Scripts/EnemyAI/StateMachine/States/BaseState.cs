using _Project.Scripts.Entities.UFO;

namespace _Project.Scripts.EnemyAI.StateMachine.States
{
    public abstract class BaseState : IState 
    {
        protected readonly UFO _enemy;
        private string Name { get; }

        protected BaseState(UFO enemy, string name = "Base State")
        {
            _enemy = enemy;
            Name = name;
        }
        
        public virtual void FixedUpdate()
        {
        }

        public virtual void OnEnter()
        {
        }
    }
}