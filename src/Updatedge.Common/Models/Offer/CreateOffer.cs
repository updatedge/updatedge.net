using System;
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
        /// Optional organisation to which <see cref="CreatedByUserId"/> belongs to.
        /// </summary>
        public string CreatedByOrgId { get; set; }

        /// <summary>
        /// Short ID of the offer used for external references
        /// </summary>
        public string ShortOfferId { get; set; }

        /// <summary>
        /// [optional] The reason ID for the offer
        /// </summary>
        public int? ReasonId { get; set; }

        /// <summary>
        /// [optional] The reason name for the offer (if not using a reason ID)
        /// </summary>
        public string ReasonName { get; set; }

        /// <summary>
        /// Time in seconds after which a reminder should be sent if offer is unanswered
        /// </summary>
        public int? ReminderInSecondsIfUnanswered { get; set; }

        /// <summary>
        /// Users to copy into offer replies
        /// </summary>
        public List<CCResponseDetails> CCResponses { get; set; }

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
        /// The Ids of the workers to send the offer to
        /// </summary>
        [Obsolete("Use WorkerIds instead")]
        public IEnumerable<string> WorkerIds { get; set; }

        /// <summary>
        /// Gets or sets the collection of worker details relating to this offer.
        /// If the WorkerId is known, set only the Id property.  If not, set the Name and Email properties instead.
        /// </summary>
        public IEnumerable<WorkerDetails> Workers { get; set; }

        /// <summary>
        /// The dates and times of the events to offer
        /// </summary>
        public IEnumerable<OfferEvent> Events { get; set; }

        /// <summary>
        /// Adds a custom reference identifier to the offer (in the context of the creating org)
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Whether this offer should be directly inserted into the worker's timeline
        /// </summary>
        public bool DirectInsertion { get; set; }

        /// <summary>
        /// Whether the offer should auto-complete on creation
        /// </summary>
        public bool AutoComplete { get; set; }

        /// <summary>
        /// The date when the offer will be auto-declined
        /// </summary>
        public DateTimeOffset? Deadline { get; set; }

        /// <summary>
        /// Where the booking takes place
        /// </summary>
        public CreateOfferLocation Address { get; set; }

        /// <summary>
        /// Details of the organisation the offer is being made on behalf of
        /// </summary>
        public CreateOfferOnBehalfOfDetails OnBehalfOf { get; set; }

        /// <summary>
        /// Financial details related to the offer
        /// </summary>
        public CreateOfferFinancialDetails FinancialDetails { get; set; }

        public class CCResponseDetails
        {
            // Id of the worker that triggers the response if responding
            public string WorkerId { get; set; }

            // Id of the UE User that receives a copy of the response
            public string UserId { get; set; }

        }

        /// <summary>
        /// Represents a worker with an identifier, name, and email address.
        /// </summary>
        /// <remarks>This class is used to store and manage information about individual workers. Each
        /// worker is identified by a unique <see cref="Id"/>, and their contact details are provided through the <see
        /// cref="Name"/> and <see cref="Email"/> properties.</remarks>
        public class WorkerDetails
        {
            /// <summary>
            /// Gets or sets the unique identifier for the worker.
            /// </summary>
            public string Id { get; set; }

            /// <summary>
            /// Gets or sets the name of the worker
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the email address associated with the worker.
            /// </summary>
            /// <remarks>The email address must be a valid, properly formatted string. It is
            /// recommended to validate the format before setting this property.</remarks>
            public string Email { get; set; }

        }

        public class CreateOfferOnBehalfOfDetails
        {
            /// <summary>
            /// Value the external system uses as an id for the hirer
            /// </summary>
            public string ExternalHirerId { get; set; }

            /// <summary>
            /// Value the external system uses as a name for the hirer
            /// </summary>
            public string ExternalHirerName { get; set; }

            /// <summary>
            /// Email used to identify hirer from calling system
            /// </summary>
            public string ExternalHirerEmail { get; set; }

            /// <summary>
            /// Id used to identify subject from calling system
            /// </summary>
            public string ExternalSubjectId { get; set; }

            /// <summary>
            /// Subject name used to identify subject from calling system
            /// </summary>
            public string ExternalSubjectName { get; set; }

            public string ExternalHirerWebsiteUrl { get; set; }

            /// <summary>
            /// The identity source that the external provider an UE have in common (e.g. GIAS)
            /// </summary>
            public string ExternalHirerCommonIdentitySource { get; set; }

            /// <summary>
            /// The name of the id field in the common identity source
            /// </summary>
            public string ExternalHirerCommonIdentityIdName { get; set; }

            /// <summary>
            /// The value of the id field in the common identity source
            /// </summary>
            public string ExternalHirerCommonIdentityIdValue { get; set; }

            public string EmailDomain { get; set; }

            public CreateOfferLocation Address { get; set; }
        }

        public class CreateOfferLocation
        {
            public string LocationPlaceName { get; set; }

            public string Address1 { get; set; }

            public string Address2 { get; set; }

            public string Address3 { get; set; }

            public string Town { get; set; }

            public string County { get; set; }

            public string PostCode { get; set; }

            public string CountryCode { get; set; }

            public double? Latitude { get; set; }

            public double? Longitude { get; set; }
        }

        public class CreateOfferFinancialDetails
        {
            /// <summary>
            ///  The current in which the worker is being paid
            /// </summary>
            public string CurrencyCode { get; set; }

            /// <summary>
            /// The gross pay should the offer be confirmed
            /// </summary>
            public decimal? TotalGrossPay { get; set; }

            /// <summary>
            /// The gross charge to should the offer be confirmed
            /// </summary>
            public decimal? TotalGrossCharge { get; set; }

        }

        public class OfferEvent : Interval
        {
            public decimal? GrossPay { get; set; }

            public decimal? GrossCharge { get; set; }

        }
    }
}
