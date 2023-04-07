namespace Web.CounterQueueExample.Services
{
    public class QueueException : Exception
    {
        public QueueException(string message) : base(message)
        { }
    }
}
