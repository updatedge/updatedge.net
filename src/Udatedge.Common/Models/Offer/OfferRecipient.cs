namespace Udatedge.Common.Models.Offer
{
    /// <summary>
    /// Recipient of an offer
    /// </summary>
    public class OfferRecipient
    {
        /// <summary>
        /// The offer-recipient ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The worker's ID
        /// </summary>
        public string WorkerId { get; set; }

        /// <summary>
        /// The worker's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The worker's response
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        /// Your confirmation of the recipient
        /// </summary>
        public string Confirmation { get; set; }
    }
}
