using System;

namespace Updatedge.Common.Models.Workers
{
    public class CreateScreeningPeriodModel
    {
        /// <summary>
        /// The type of verification
        /// </summary>
        public VerificationType TypeId { get; set; }

        /// <summary>
        /// Contact id of the verified contact
        /// </summary>
        public string VerifiedContactId { get; set; }

        /// <summary>
        /// The start date of the verification
        /// </summary>
        public DateTimeOffset? Start { get; set; }

        /// <summary>
        /// RejectionReason
        /// </summary>
        public string RejectionReason { get; set; }
    }

    public enum VerificationType : int
    {
        Rejected = 0,
        Identity = 1,
        Representation = 2,
        Vetting = 3,
        Suspension = 4,
    }
}
