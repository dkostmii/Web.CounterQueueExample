using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace Web.CounterQueueExample.Services
{
    public class CounterQueue : ICounterQueue
    {
        private readonly ConcurrentQueue<ICounterOperation> _workOperations = new();
        private readonly CounterQueueOptions _options;
        private readonly ILogger<CounterQueue> _logger;

        public bool IsEmpty => _workOperations.IsEmpty;

        public CounterQueue(ILogger<CounterQueue> logger, IOptions<CounterQueueOptions> options)
        {
            _logger = logger;
            _options = options.Value;
        }

        public void Queue(ICounterOperation operation)
        {
            _logger.LogDebug("Trying to enqueue operation {operation}", operation);

            if (_workOperations.Count == _options.MaxQueuedOperations)
            {
                var error = new WorkOperationsLimitException(_options.MaxQueuedOperations);

                _logger.LogError("Error occurred while enqueueing operation {operation}: {error}", operation, error);
                throw error;
            }

            _workOperations.Enqueue(operation);

            _logger.LogDebug("Successfully enqueued operation {operation}", operation);
        }

        public ICounterOperation Dequeue()
        {
            _logger.LogDebug("Trying to dequeue operation.");

            var success = _workOperations.TryDequeue(out var workOperation);

            if (!success)
            {
                var error = new QueueIsEmptyException();
                _logger.LogDebug("Error occurred while dequeueing operation: {error}", error);

                throw error;
            }

            _logger.LogDebug("Successfully dequeued operation {workOperation}", workOperation);

            return workOperation!;
        }
    }
}
