namespace _Project.Scripts.Services.Awards
{
    public interface IAwardGiver<T>
    {
        void GiveAwardFor(T entityDestroyedEvent);
    }
}