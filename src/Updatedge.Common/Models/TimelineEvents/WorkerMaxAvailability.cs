using System;

namespace Updatedge.Common.Models.TimelineEvents
{
    /// <summary>
    /// Holds the point in time at which a worker becomes available again (via inferred unavailability)
    /// </summary>
    public class WorkerMaxAvailability
    {
        /// <summary>
        /// Id of worker
        /// </summary>
        public string WorkerId { get; set; }

        /// <summary>
        /// When the worker becomes available.
        /// </summary>
        public DateTimeOffset MaxAvailabilityEndDate { get; set; }
    }
}
