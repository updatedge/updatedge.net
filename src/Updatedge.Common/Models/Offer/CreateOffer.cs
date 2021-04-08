using System.Collections.Generic;
using Updatedge.Common.Models.Availability;

namespace Updatedge.Common.Models.Offer
{
    /// <summary>
    /// Create offer model
    /// </summary>
    public class CreateOffer
    {
        /// <summary>
        /// The Id of the user to send the offer from
        /// </summary>
        public string CreatedByUserId { get; set; }

        /// <summary>
        /// Title of the offer
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// [optional] Details of the offer
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// [optional] A Google PlaceId for the offer (a map will show in the offer if this is set)
        /// </summary>
        public string LocationPlaceId { get; set; }

        /// <summary>
        /// [optional] Longitude coordinate for the place of the offer
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        /// [optional] Latitude  coordinate for the place of the offer
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// The Ids of the workers to send the offer to
        /// </summary>
        public IEnumerable<string> WorkerIds { get; set; }

        /// <summary>
        /// The dates and times of the events to offer
        /// </summary>
        public IEnumerable<BaseInterval> Events { get; set; }
    }
}
