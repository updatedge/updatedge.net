using System;
using System.Collections.Generic;
using System.Text;
using static Updatedge.Common.Models.Offer.CreateOffer;

namespace Updatedge.Common.Models.Offer
{
    public class CompleteOfferRequest
    {
        public string Id { get; set; }
        public IEnumerable<string> WorkerIds { get; set; }

        public IEnumerable<WorkerDetails> Workers { get; set; }

        /// <summary>
        /// Short ID of the offer used for external references
        /// </summary>
        public string ShortOfferId { get; set; }
        public decimal? TotalGrossPay { get; set; }
        public decimal? TotalGrossCharge { get; set; }
        public List<CreateOffer.OfferEvent> Events { get; set; }
    }
}
