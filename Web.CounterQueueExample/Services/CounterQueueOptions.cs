using System.ComponentModel.DataAnnotations;

namespace Web.CounterQueueExample.Services
{
    public class CounterQueueOptions
    {
        public const string CounterQueue = "CounterQueue";

        [Required]
        [Range(1, 20)]
        public int MaxQueuedOperations { get; set; }
    }
}
