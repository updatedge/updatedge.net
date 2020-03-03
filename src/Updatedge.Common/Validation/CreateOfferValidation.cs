using Updatedge.Common.Models.Offer;

namespace Updatedge.Common.Validation
{
    /// <summary>
    /// Validates a <see cref="CreateOffer"/> object.
    /// </summary>
    public class CreateOfferValidation : BaseValidations
    {
        private readonly CreateOffer _offer;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="offer">Offer to be validated</param>
        public CreateOfferValidation(CreateOffer offer) : base()
        {
            _offer = offer;
        }

        /// <summary>
        /// Ensure title not null or empty
        /// </summary>
        /// <returns>Itself</returns>
        public CreateOfferValidation TitleNotNullOrEmpty()
        {
            if (string.IsNullOrEmpty(_offer.Title))
                Add(nameof(_offer.Title), Constants.ErrorMessages.ValueNotSpecified);

            return this;
        }

        /// <summary>
        /// Ensure createdByUserId not null or empty
        /// </summary>
        /// <returns>Itself</returns>
        public CreateOfferValidation CreatedByUserIdNotNullOrEmpty()
        {

            if (string.IsNullOrEmpty(_offer.CreatedByUserId))
                Add(nameof(_offer.CreatedByUserId), Constants.ErrorMessages.ValueNotSpecified);

            return this;
        }

    }
}
