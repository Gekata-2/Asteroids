using _Project.Scripts.Entities.UFO;

namespace _Project.Scripts.EnemyAI.StateMachine.States
{
    public abstract class BaseState : IState 
    {
        protected readonly UFO Ufo;

        protected BaseState(UFO ufo)
        {
            Ufo = ufo;
        }
        
        public virtual void FixedUpdate()
        {
        }

        public virtual void OnEnter()
        {
        }
    }
}