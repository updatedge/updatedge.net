using System;

namespace Updatedge.net.Entities.V1.Availability
{
    /// <summary>
    /// Defines the start and end points of a period of time
    /// </summary>
    public class Interval
    {
        /// <summary>
        /// Start of interval
        /// </summary>
        public DateTimeOffset Start { get; set; }

        /// <summary>
        /// End of interval
        /// </summary>
        public DateTimeOffset End { get; set; }

        /// <summary>
        /// Total number of minutes between start and end
        /// </summary>
        public int IntervalMinutes { get; set; }
    }
}
