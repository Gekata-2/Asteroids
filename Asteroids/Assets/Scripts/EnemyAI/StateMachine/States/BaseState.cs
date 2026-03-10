using Entities.UFO;

namespace EnemyAI.StateMachine.States
{
    public abstract class BaseState : IState 
    {
        protected readonly UFO _enemy;
        public string Name { get; }


        public BaseState(UFO enemy, string name = "Base State")
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