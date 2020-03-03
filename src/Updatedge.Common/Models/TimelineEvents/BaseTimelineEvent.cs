using System;

namespace Updatedge.Common.Models.TimelineEvents
{
    /// <summary>
    /// Defines a
    /// </summary>
    public class BaseTimelineEvent
    {
        /// <summary>
        /// Id of event
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of event type
        /// </summary>
        public string EventType { get; set; }

        /// <summary>
        /// Start of event
        /// </summary>
        public DateTimeOffset Start { get; set; }

        /// <summary>
        /// End of event
        /// </summary>
        public DateTimeOffset End { get; set; }

        /// <summary>
        /// Name of repeat type
        /// </summary>
        public string RepeatType { get; set; }

        /// <summary>
        /// When event was created
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// When event was last modified
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

    }
}
