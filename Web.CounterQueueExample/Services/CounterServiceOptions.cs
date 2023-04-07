using System.ComponentModel.DataAnnotations;

namespace Web.CounterQueueExample.Services
{
    public class CounterServiceOptions
    {
        public const string CounterService = "CounterService";

        [Required]
        [Range(100, 5000)]
        public int IntervalMilliseconds { get; set; }
    }
}
