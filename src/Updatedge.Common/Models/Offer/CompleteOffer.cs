using System;
using System.Collections.Generic;
using System.Text;

namespace Updatedge.Common.Models.Offer
{
    public class CompleteOfferRequest
    {
        public string Id { get; set; }
        public IEnumerable<string> WorkerIds { get; set; }

        /// <summary>
        /// Short ID of the offer used for external references
        /// </summary>
        public string ShortOfferId { get; set; }
        public decimal? TotalGrossPay { get; set; }
        public decimal? TotalGrossCharge { get; set; }
        public List<CreateOffer.OfferEvent> Events { get; set; }
    }
}
