using Entities.UFO;

namespace EnemyAI.StateMachine.States
{
    public class IdleState : BaseState
    {
        public IdleState(UFO enemy) : base(enemy, "Idle")
        {
        }
    }
}