using System;
using System.Collections.Generic;
using Updatedge.Common.Models.Workers;

namespace Updatedge.Common.Models.Availability
{
    public class WorkersAvailabilityUrlRequest
    {
        public string AgencyContactEmail { get; set; }
        public string MetadataSource { get; set; }

        public string Urn { get; set; }

        public DateTimeOffset Start { get; set; }

        public int Days { get; set; }

        public ICollection<WorkerRated> Workers { get; set; }
    }
}
