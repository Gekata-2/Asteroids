using Entities.UFO;

namespace EnemyAI.StateMachine.States
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

        public string GetName()
            => Name;

        public virtual void Update()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void OnEnter()
        {
        }

        public virtual void OnExit()
        {
        }
    }
}