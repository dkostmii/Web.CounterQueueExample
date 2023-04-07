namespace Web.CounterQueueExample.Services
{
    public interface ICounterQueue
    {
        bool IsEmpty { get; }
        void Queue(ICounterOperation operation);
        ICounterOperation Dequeue();
    }
}
