using _Project.Scripts.Entities.UFO;

namespace _Project.Scripts.EnemyAI.StateMachine.States
{
    public class DieState : BaseState
    {
        public DieState(UFO enemy) : base(enemy, "Die")
        {
        }

        public override void OnEnter()
        {
            _enemy.Die();
        }
        
    }
}