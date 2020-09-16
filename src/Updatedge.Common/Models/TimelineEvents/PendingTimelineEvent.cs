using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
