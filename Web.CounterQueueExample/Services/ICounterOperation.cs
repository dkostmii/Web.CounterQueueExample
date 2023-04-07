namespace Web.CounterQueueExample.Services
{
    public interface ICounterOperation
    {
        int PerformOperation(int value);
        string ToString();
    }
}
