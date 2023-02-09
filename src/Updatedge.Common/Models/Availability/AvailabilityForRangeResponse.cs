using System;
using System.Collections.Generic;

namespace Updatedge.Common.Models.Availability
{
    public class AvailabilityForRangeResponse
    {
        public List<AvailabilityForRange> AvailabilityForRange { get; set; } = new List<AvailabilityForRange>();
    }

    public class AvailabilityForRange
    {
        public int Count { get; set; }

        public DateTimeOffset Date { get; set; }

        public List<AvailabilityForRangeWorker> Workers { get; set; } = new List<AvailabilityForRangeWorker>();
    }

    public class AvailabilityForRangeWorker
    {
        public string WorkerId { get; set; }
        public DateTimeOffset? LastTimelineUpdate { get; set; }
        public int TotalMinutesAvailable { get; set; }
    }
}
