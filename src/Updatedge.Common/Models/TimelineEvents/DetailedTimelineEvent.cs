using System;

namespace Updatedge.Common.Models.TimelineEvents
{
    /// <summary>
    /// Defines a
    /// </summary>
    public class DetailedTimelineEvent : BaseTimelineEvent
    {
        /// <summary>
        /// Id of worker event belongs to.
        /// </summary>
        public string WorkerId { get; set; }

        /// <summary>
        /// Uniquely generated id for a repeat event (if event is a repeated instance)
        /// </summary>
        public string InstanceId { get; set; }

        /// <summary>
        /// Id of event type
        /// </summary>
        public int EventTypeId { get; set; }

        /// <summary>
        /// Id of repeat pattern
        /// </summary>
        public int RepeatTypeId { get; set; }

        /// <summary>
        /// Whether repeats forever
        /// </summary>
        public bool RepeatForever { get; set; }

        /// <summary>
        /// When event repeats until (if applicable)
        /// </summary>
        public DateTimeOffset RepeatUntil { get; set; }

        /// <summary>
        /// Whether the event repeats
        /// </summary>
        public bool IsRepeatEvent => RepeatTypeId > 0;

        /// <summary>
        /// Whether the event has been deleted
        /// </summary>
        public bool Deleted { get; set; }


    }
}
