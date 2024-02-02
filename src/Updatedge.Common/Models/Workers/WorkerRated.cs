using System.Collections.Generic;
using System.Linq;
using Updatedge.Common.Models.TimelineEvents;

namespace Updatedge.Common.Models.Workers
{
    public class WorkerRated : BaseWorker
    {
        public int Rating { get; set; }

        public string WorkerId { get; set; }

        public string EmailAddress { get; set; }

        public decimal TotalHours
        {
            get { return Events.Sum(span => span.Hours); }
        }

        /// <summary>
        /// Total gross charge to hirer for this worker
        /// </summary>
        public double TotalGrossCharge { get; set; }

        public ICollection<PendingTimelineEvent> Events { get; set; }
    }
}
