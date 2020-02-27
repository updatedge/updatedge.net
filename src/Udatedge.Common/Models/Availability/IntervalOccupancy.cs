using System;

namespace Udatedge.Common.Models.Availability
{
    /// <summary>
    /// Adds occupancy properties to a <see cref="Interval"/>
    /// </summary>
    public class IntervalOccupancy : Interval
    {
        /// <summary>
        /// Total number of minutes occupied in this slot by worker
        /// </summary>
        public double OccupiedMinutes { get; set; }

        /// <summary>
        /// Percentage of minutes occupied in interval by worker
        /// </summary>
        public double OccupiedPercentage => Math.Round(OccupiedMinutes / IntervalMinutes * 100, 2);
    }

}
