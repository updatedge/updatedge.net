using Light.GuardClauses;
using System;

namespace Updatedge.Common.Models.TimelineEvents
{
    public class TimeFrame
    {
        public TimeFrame() // parameterless c'tor is mandatory for API deserialisation usage
        {
            Start = DateTimeOffset.MinValue;
            End = DateTimeOffset.MinValue;
        }

        public DateTimeOffset Start { get; set; }

        public DateTimeOffset End { get; set; }

        /// <summary>
        /// Defines a period of time delineated by two points in time.
        /// </summary>
        /// <param name="start">Start of time frame</param>
        /// <param name="end">End of time frame</param>
        public TimeFrame(DateTimeOffset start, DateTimeOffset end)
        {
            end.MustBeGreaterThan(start);

            Start = start;
            End = end;
        }

        /// <summary>
        /// Timespan between start and end
        /// </summary>
        public TimeSpan Length => End.Subtract(Start);
    }
}