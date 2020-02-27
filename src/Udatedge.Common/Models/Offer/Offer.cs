using System;
using System.Collections.Generic;
using Udatedge.Common.Models.Availability;

namespace Udatedge.Common.Models.Offer
{
    /// <summary>
    /// Offer model
    /// </summary>
    public class Offer
    {
        /// <summary>
        /// ID of the offer
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Title of the offer
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Details of the offer
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// Whether the offer is in the past
        /// </summary>
        public bool IsHistoric { get; set; }

        /// <summary>
        /// Whether the offer has been withdrawn
        /// </summary>
        public bool Widthdrawn { get; set; }

        /// <summary>
        /// Whether the offer has been complete
        /// </summary>
        public bool Complete { get; set; }

        /// <summary>
        /// Google PlaceId for the offer
        /// </summary>
        public string LocationPlaceId { get; set; }

        /// <summary>
        /// When the offer was created
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Events for the offer
        /// </summary>
        public IEnumerable<Interval> Events { get; set; }

        /// <summary>
        /// Recipients of the offer
        /// </summary>
        public IEnumerable<OfferRecipient> Recipients { get; set; }
    }
}
