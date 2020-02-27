using System.Collections.Generic;

namespace Udatedge.Common.Models.Availability
{
    /// <summary>
    /// Holds incoming parameters when a request to calculate availability over a set of intervals for workers is received.
    /// </summary>
    public class WorkersIntervalsRequest
    {
        /// <summary>
        /// A list of worker ids.
        /// </summary>
        public List<string> WorkerIds { get; set; }

        /// <summary>
        /// A list of intervals.
        /// </summary>
        public List<BaseInterval> Intervals { get; set; }
    }
}
