using _Project.Scripts.Entities.UFO;

namespace _Project.Scripts.EnemyAI.StateMachine.States
{
    public class DieState : BaseState
    {
        public DieState(UFO ufo) : base(ufo)
        {
        }

        public override void OnEnter()
        {
            Ufo.Die();
        }
        
    }
}