namespace Services.Awards
{
    public interface IAwardGiver<T>
    {
        void GiveAwardFor(T timeLivedEvent);
    }
}