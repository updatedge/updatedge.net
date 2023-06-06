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
        /// Value used to identify the hirer from calling system
        /// </summary>
        public string ExternalHirerId { get; set; }

        /// <summary>
        /// Name used to identify hirer from calling system
        /// </summary>
        public string ExternalHirerName { get; set; }

        /// <summary>
        /// Id used to identify subject from calling system
        /// </summary>
        public string ExternalSubjectId { get; set; }

        /// <summary>
        /// Subject name used to identify subject from calling system
        /// </summary>
        public string ExternalSubjectName { get; set; }

        /// <summary>
        /// Title of the offer
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The domain name of the organisation in order to enable logo lookup
        /// </summary>
        public string OrganisationUrl { get; set; }

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
        /// Financial details related to the offer
        /// </summary>
        public CreateOfferFinancialDetails FinancialDetails { get; set; }

        /// <summary>
        /// The Ids of the workers to send the offer to
        /// </summary>
        public IEnumerable<string> WorkerIds { get; set; }

        /// <summary>
        /// The dates and times of the events to offer
        /// </summary>
        public IEnumerable<BaseInterval> Events { get; set; }
        
        /// <summary>
        /// Whether this offer should be directly inserted into the worker's timeline
        /// </summary>
        public bool DirectInsertion { get; set; }

        /// <summary>
        /// Whether the offer should auto-complete on creation
        /// </summary>
        public bool AutoComplete { get; set; }

        public class CreateOfferFinancialDetails
        {
            /// <summary>
            /// The gross pay should the offer be confirmed
            /// </summary>
            public decimal TotalGrossPay { get; set; }
        }

        /// <summary>
        /// Details of the organisation the offer is being made on behalf of
        /// </summary>
        public CreateOfferExternalOrganisationDetails ExternalOrganisationDetails { get; set; }


        public class CreateOfferExternalOrganisationDetails
        {
            public string ExternalOrganisationId { get; set; }

            public string Name { get; set; }

            public string WebsiteUrl { get; set; }

            public string EmailDomain { get; set; }

            public string Base64Logo { get; set; }

            public string LocationPlaceName { get; set; }

            public string Address1 { get; set; }

            public string Address2 { get; set; }

            public string Address3 { get; set; }

            public string Town { get; set; }

            public string County { get; set; }

            public string PostCode { get; set; }

            public string CountryCode { get; set; }

            public float Latitude { get; set; }

            public float Longitude { get; set; }
        }
    }
}
