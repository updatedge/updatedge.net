using System.Collections.Generic;
using Updatedge.Common.Enumerations;

namespace Updatedge.Common.Models.Rating
{
    // unused

    public class PostRatingRequest
    {
        public RatingDirection RatingDirection { get; set; }
        public string ProviderOrgId { get; set; }
        public string ProvideeOrgName { get; set; }
        public string ProvideeOrgDomain { get; set; }
        //public string RecipientOrgId { get; set; } N.B. removed inappropriate model property
        public string RaterOrgId { get; set; }
        public string RaterExternalId { get; set; }
        public string Token { get; set; }
        public string RaterExternalOrgId { get; set; }
        public IEnumerable<RatingPost> Ratings { get; set; }
    }

    public class RatingPost
    {
        public string UserId { get; set; }
        public string OrgId { get; set; }
        public int Stars { get; set; }
        public string PublicComment { get; set; }
        public string PrivateComment { get; set; }
        public string RecipientOrgId { get; set; }
        public string RecipientExternalId { get; set; }
        public string RecipientContactId { get; set; }
        public string RecipientExternalOrgId { get; set; }
        public string ProviderOrgId { get; set; }
    }
}
