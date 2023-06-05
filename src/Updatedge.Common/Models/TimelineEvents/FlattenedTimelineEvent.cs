using System;
using System.Collections.Generic;
using System.Text;

namespace Updatedge.Common.Models.TimelineEvents
{
    public enum FreeBusyStatus
    {
        Free = 1,
        Busy = 2
    }

    public class FlattenedTimelineEvent
    {
        public string WorkerId { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
        public int EventTypeId { get; set; }
        public string EventType { get; set; }
        public FreeBusyStatus Status { get; set; }
    }
}
