using System;
using System.Collections.Generic;
using System.Text;

namespace Updatedge.Common.Models.Offer
{
    public class CreateOfferEvent
    {
        public string OfferId { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
        public int AdjustedLengthMins { get; set; }
    }
}
