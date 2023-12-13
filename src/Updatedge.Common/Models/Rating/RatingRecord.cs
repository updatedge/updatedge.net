using System;
using Updatedge.Common.Enumerations;

namespace Updatedge.Common.Models.Rating
{
    // unused

    public class RatingRecord
    {
        public int Id { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public int RatingType { get; set; }
        public RatingDirection RatingDirection { get; set; }
        public bool Published { get; set; }

        public int Stars { get; set; }
        public string PublicComment { get; set; }
        public string PrivateComment { get; set; }

        public string ProviderOrgId { get; set; }
        public string ProviderOrgName { get; set; }

        public string RaterContactId { get; set; }
        public string RaterExternalId { get; set; }
        public string RaterExternalOrgId { get; set; }
        public string RaterExternalOrgName { get; set; }
        public string RaterExternalOrgDomain { get; set; }
        public string RaterOrgId { get; set; }

        public string RecipientContactId { get; set; }
        public string RecipientExternalId { get; set; }
        public string RecipientExternalOrgName { get; set; }
        public string RecipientExternalOrgDomain { get; set; }
        public string RecipientExternalOrgId { get; set; }
        public string RecipientOrgId { get; set; }

        public bool ModeratorApproved { get; set; }
        public bool RecipientApproved { get; set; }
        public bool RecipientHidden { get; set; }
        public string ReportedByUserId { get; set; }
        public bool ExternalSource { get; set; }
    }

    public class RatingRecordMeta : RatingRecord
    {
        public string RaterUserId { get; set; }
        public string RaterFirstName { get; set; }
        public string RaterLastName { get; set; }
        public string RaterOrganisationName { get; set; }
        public string RaterOrganisationId { get; set; }
        public string RecipientUserId { get; set; }
        public string RecipientFirstName { get; set; }
        public string RecipientLastName { get; set; }
        public string RecipientOrganisationName { get; set; }
        public string ReportedByUserFirstName { get; set; }
        public string ReportedByUserLastName { get; set; }
    }

    public class AvgAndTotalRating
    {
        public string UserId { get; set; }
        public int TotalRatings { get; set; }
        public float AvgRating { get; set; }
    }

    public class RatingQuery
    {
        public RatingDirection RatingDirection { get; set; }
        public string UserId { get; set; }
        public string ContactId { get; set; }
        public string RecipientOrgId { get; set; }
    }

    public class UnmoderatedRatingUpdate
    {
        public string PublicComment { get; set; }

        public string PrivateComment { get; set; }
    }

    public class RaterDetail
    {
        public string Id { get; set; }
        public string ContactId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }
}