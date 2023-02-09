using System;
using System.Collections.Generic;
using System.Text;

namespace Updatedge.Common.Models.Offer
{
    public class AlterOffer
    {
        public List<string> WorkerIds { get; set; }

        public DateTimeOffset? Deadline { get; set; }
    }
}
