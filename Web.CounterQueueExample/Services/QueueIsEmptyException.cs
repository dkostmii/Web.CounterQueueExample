namespace Web.CounterQueueExample.Services
{
    public class QueueIsEmptyException : QueueException
    {
        public QueueIsEmptyException() : base(
            "No enqueued operations available.")
        { }
    }
}
