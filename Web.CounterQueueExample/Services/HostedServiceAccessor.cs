namespace Web.CounterQueueExample.Services
{
    public class HostedServiceAccessor<T> : IHostedServiceAccessor<T> where T : IHostedService
    {
        public T Service { get; }

        public HostedServiceAccessor(IEnumerable<IHostedService> hostedServices)
        {
            var service = hostedServices.FirstOrDefault(s => s is T);

            ArgumentNullException.ThrowIfNull(service, nameof(service));

            if (service is T match)
                Service = match;
            else
                throw new ArgumentException("This error should not occur. Expected valid type of IHostedService. Got: " + service.GetType().Name);
        }
    }
}
