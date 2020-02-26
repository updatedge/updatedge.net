using System;

namespace Udatedge.Common.Models.TimelineEvents
{
    /// <summary>
    /// Defines a timeline response model describing an event on a worker's timeline.
    /// </summary>
    public class TimelineEvent : BaseTimelineEvent
    {
        /// <summary>
        /// Uniquely generated id for a repeat event (if event is a repeated instance)
        /// </summary>
        public string InstanceId { get; set; }

        /// <summary>
        /// Whether the event is a repeat event
        /// </summary>
        public bool Repeats { get; set; }

        /// <summary>
        /// When event repeats until (if applicable)
        /// </summary>
        public DateTimeOffset? RepeatUntil { get; set; }

        /// <summary>
        /// Whether this event is an instance of the original event
        /// </summary>
        public bool? RepeatInstance { get; set; }


    }
}
