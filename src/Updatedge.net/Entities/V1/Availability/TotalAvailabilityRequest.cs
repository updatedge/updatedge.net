using System;
using System.Collections.Generic;
using System.Text;

namespace Updatedge.net.Entities.V1.Availability
{
    public class TotalAvailabilityRequest
    {
        public List<string> WorkerIds { get; set; }

        public List<Interval> Intervals { get; set; }
    }

}
