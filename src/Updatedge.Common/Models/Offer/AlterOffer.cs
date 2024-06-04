using System;
using System.Collections.Generic;
using System.Text;
using Updatedge.Common.Models.Availability;

namespace Updatedge.Common.Models.Offer
{
    public class AlterOffer
    {
        public string AlteredByUserId { get; set; }

        public List<string> WorkerIds { get; set; }

        public DateTimeOffset? Deadline { get; set; }

        public List<Interval> Events { get; set; }
    }
}
