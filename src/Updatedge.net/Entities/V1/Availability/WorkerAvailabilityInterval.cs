using System;
using System.Collections.Generic;
using System.Text;

namespace Updatedge.net.Entities.V1.Availability
{
    /// <summary>
    /// A worker and their periods of availability
    /// </summary>
    public class WorkerAvailabilityIntervals
    {
        /// <summary>
        /// Id of worker to which these intervals apply.
        /// </summary>
        public string WorkerId { get; set; }

        /// <summary>
        /// Intervals of time in which the worker is unavailable.
        /// </summary>
        public List<Interval> UnavailableIntervals { get; set; }

        /// <summary>
        /// Intervals in which the worker has stated they are explicitly available for work.
        /// </summary>
        public List<Interval> AvailableNowIntervals { get; set; }
    }

}
