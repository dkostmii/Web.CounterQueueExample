namespace Web.CounterQueueExample.Services
{
    public class WorkOperationsLimitException : QueueException
    {
        public WorkOperationsLimitException(int maxWorkOperations) : base(
            $"Cannot enqueue operation. Max count of work operations limit exceeded. The limit is: {maxWorkOperations}")
        { }
    }
}
