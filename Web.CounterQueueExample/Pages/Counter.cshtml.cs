using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.CounterQueueExample.Services;

namespace Web.CounterQueueExample.Pages
{
    public class CounterModel : PageModel
    {
        private readonly CounterService _service;
        private readonly ICounterQueue _queue;
        private readonly string[] _operations;

        public CounterModel(
            ICounterQueue queue,
            IHostedServiceAccessor<CounterService> accessor)
        {
            (_queue, _service) = (queue, accessor.Service);
            _operations = CounterOperation.Operations.Order().ToArray();
        }

        public int Count => _service.Count;
        public string[] Operations => _operations;

        public string? Error { get; set; } = null;
        public string? Message { get; set; } = null;

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost(string operationName)
        {
            Error = null;
            Message = null;

            try
            {
                var operation = CounterOperation.GetOperation(operationName);
                _queue.Queue(operation);

                Message = "Operation queued successfully.";
            }
            catch (InvalidCounterOperationException ex)
            {
                Error = ex.Message;
            }
            catch (QueueException ex)
            {
                Error = ex.Message;
            }

            return Page();
        }
    }
}
