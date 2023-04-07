using Microsoft.Extensions.Options;

namespace Web.CounterQueueExample.Services
{
    public class CounterService : BackgroundService
    {
        private readonly ILogger<CounterService> _logger;
        private readonly ICounterQueue _counterQueue;
        private readonly CounterServiceOptions _options;

        private readonly PeriodicTimer _timer;

        private int _count;

        public int Count => _count;

        public CounterService(
            ILogger<CounterService> logger,
            ICounterQueue counterQueue,
            IOptions<CounterServiceOptions> options)
        {
            (_count, _logger, _counterQueue, _options) = (0, logger, counterQueue, options.Value);

            _timer = new(TimeSpan.FromMilliseconds(_options.IntervalMilliseconds));
        }
            
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var selfName = nameof(CounterService);

            _logger.LogInformation("{selfName} is starting...", selfName);

            while (await _timer.WaitForNextTickAsync(stoppingToken)
                   && !stoppingToken.IsCancellationRequested)
            {
                await DoWorkAsync();
            }

            _logger.LogInformation("{selfName} is stopping...", selfName);
        }

        private Task DoWorkAsync()
        {
            if (_counterQueue.IsEmpty)
                return Task.CompletedTask;

            try
            {
                var workOperation = _counterQueue.Dequeue();
                _count = workOperation.PerformOperation(_count);
                _logger.LogDebug("Performed operation {workOperation}. New count {_count}", workOperation, _count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while performing work operation.");
            }

            return Task.CompletedTask;
        }
    }
}
