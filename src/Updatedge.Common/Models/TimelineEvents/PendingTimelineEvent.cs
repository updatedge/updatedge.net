using System;

namespace Updatedge.Common.Models.TimelineEvents
{
    public class PendingTimelineEvent
    {

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Start of event
        /// </summary>
        public DateTimeOffset Start { get; set; }

        /// <summary>
        /// End of event
        /// </summary>
        public DateTimeOffset End { get; set; }

        /// <summary>
        /// User Id of intended recipient
        /// </summary>
        public string RecipientUserId { get; set; }

        /// <summary>
        /// Group Id
        /// </summary>
        public string GroupId { get; set; }
        
        /// <summary>
        /// Hours worked
        /// </summary>
        public decimal Hours => (decimal)(End - Start).TotalHours;
    }
}
