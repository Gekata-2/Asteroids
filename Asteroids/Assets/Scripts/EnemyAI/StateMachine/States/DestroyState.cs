using Entities.UFO;

namespace EnemyAI.StateMachine.States
{
    public class DestroyState : BaseState
    {
        public DestroyState(UFO enemy) : base(enemy, "Destroy")
        {
        }

        public override void OnEnter()
        {
            _enemy.NotifyAboutDeath();
        }
    }
}