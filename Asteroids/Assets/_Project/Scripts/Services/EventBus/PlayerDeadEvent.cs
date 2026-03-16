namespace _Project.Scripts.Services.EventBus
{
    public class PlayerDeadEvent
    {
        public readonly object Killer;

        public PlayerDeadEvent(object killer)
        {
            Killer = killer;
        }
    }
}