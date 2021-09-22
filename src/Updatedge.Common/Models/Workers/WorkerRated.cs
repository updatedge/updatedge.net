using System.Collections.Generic;
using Updatedge.Common.Models.TimelineEvents;

namespace Updatedge.Common.Models.Workers
{
    public class WorkerRated : BaseWorker
    {
        public int Rating { get; set; }

        public string WorkerId { get; set; }

        public ICollection<PendingTimelineEvent> Events { get; set; }
    }
}
