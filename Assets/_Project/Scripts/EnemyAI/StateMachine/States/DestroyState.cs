using _Project.Scripts.Entities.UFO;

namespace _Project.Scripts.EnemyAI.StateMachine.States
{
    public class DestroyState : BaseState
    {
        public DestroyState(UFO enemy) : base(enemy, "Destroy")
        {
        }

        public override void OnEnter()
        {
            _enemy.Die();
        }
    }
}