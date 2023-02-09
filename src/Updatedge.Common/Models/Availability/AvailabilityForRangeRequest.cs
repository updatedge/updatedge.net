using System;
using System.Collections.Generic;

namespace Updatedge.Common.Models.Availability
{
    public class AvailabilityForRangeRequest
    {
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public int WindowMinutes { get; set; }
        public List<string> WorkerIds { get; set; }
    }
}
