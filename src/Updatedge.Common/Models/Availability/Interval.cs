using System;

namespace Updatedge.Common.Models.Availability
{
    /// <summary>
    /// Base interval definition
    /// </summary>
    public class BaseInterval
    {
        /// <summary>
        /// Start date and time (UTC).
        /// </summary>
        public DateTimeOffset Start { get; set; }

        /// <summary>
        /// End date and time (UTC).
        /// </summary>
        public DateTimeOffset End { get; set; }
    }

    /// <summary>
    /// Defines the start and end points of a period of time
    /// </summary>
    public class Interval : BaseInterval
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Interval() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="interval"></param>
        public Interval(BaseInterval interval)
        {
            Start = interval.Start;
            End = interval.End;
        }

        /// <summary>
        /// Total number of minutes between start and end
        /// </summary>
        public int IntervalMinutes => (int)End.Subtract(Start).TotalMinutes;

        /// <summary>
        /// The length of the working period within the interval in minutes
        /// </summary>
        public int? AdjustedIntervalMinutes { get; set; }

        /// <summary>
        /// Ensures two datetimeoffsets fall on the same day
        /// </summary>
        /// <returns></returns>
        public bool EndsOnSameDay()
        {
            var returnValue = false;
            var firstAdjusted = Start.ToUniversalTime().Date;
            var secondAdjusted = End.ToUniversalTime().Date;

            // calculate the total difference between the dates   
            var diff = Start.Date.CompareTo(firstAdjusted) - End.Date.CompareTo(secondAdjusted);
            // the firstAdjusted date is corrected for the difference in BOTH dates.
            firstAdjusted = firstAdjusted.AddDays(diff);

            if (DateTime.Compare(firstAdjusted, secondAdjusted) == 0)
                returnValue = true;

            return returnValue;
        }

    }

}


