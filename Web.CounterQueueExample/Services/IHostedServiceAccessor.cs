namespace Web.CounterQueueExample.Services
{
    public interface IHostedServiceAccessor<T> where T : IHostedService
    {
        T Service { get; }
    }
}
