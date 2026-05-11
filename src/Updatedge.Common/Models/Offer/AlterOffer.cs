using System;
using System.Collections.Generic;
using System.Text;
using Updatedge.Common.Models.Availability;
using static Updatedge.Common.Models.Offer.CreateOffer;

namespace Updatedge.Common.Models.Offer
{
    public class AlterOffer
    {
        public string AlteredByUserId { get; set; }

        public List<string> WorkerIds { get; set; }

        public List<WorkerDetails> Workers { get; set; }

        public DateTimeOffset? Deadline { get; set; }

        public List<CreateOffer.OfferEvent> Events { get; set; }

        public decimal? TotalGrossPay { get; set; }

        public decimal? TotalGrossCharge { get; set; }
    }
}
