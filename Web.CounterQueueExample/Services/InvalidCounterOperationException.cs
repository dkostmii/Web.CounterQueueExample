namespace Web.CounterQueueExample.Services
{
    public class InvalidCounterOperationException : Exception
    {
        public InvalidCounterOperationException(string invalidOperationName) : base(
            $"Invalid counter operation: {invalidOperationName}.")
        { }
    }
}
