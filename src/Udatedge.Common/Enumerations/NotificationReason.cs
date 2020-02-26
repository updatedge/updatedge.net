namespace Udatedge.Common.Enumerations
{
    /// <summary>
    /// Reason for a notification
    /// </summary>
    public enum NotificationReason
    {
        /// <summary>
        /// Not specified
        /// </summary>
        NotSpecified = 0,
        /// <summary>
        /// User is being prompted to sync after inactivity
        /// </summary>
        SyncReminder = 1,
        /// <summary>
        /// User has not uploaded their profile photo since account creation
        /// </summary>
        MissingProfilePhoto = 2,
        /// <summary>
        /// User is being Nudged to update their availability
        /// </summary>
        ContactNudge = 3,
        /// <summary>
        /// An offer is being sent to a user
        /// </summary>
        OfferSent = 4,
        /// <summary>
        /// An offer has been confirmed for a user
        /// </summary>
        OfferConfirmed = 5,
        /// <summary>
        /// Worker has been unsuccessful in an offer
        /// </summary>
        OfferUnsuccessful = 6,
        /// <summary>
        /// Worker has applied for an offer
        /// </summary>
        OfferApplied = 7,
        /// <summary>
        /// Hirer is requesting assignment confirmation from third-party
        /// </summary>
        OfferRequestAssignmentConfirmation = 8,
        /// <summary>
        /// Notify hirer that a worker has deleted an offer event
        /// </summary>
        OfferEventDeleted = 9,
        /// <summary>
        /// User account was activated
        /// </summary>
        AccountActivated = 10,
        /// <summary>
        /// Password reset requested for this user
        /// </summary>
        RequestPasswordReset = 11,
        /// <summary>
        /// Contact has requested a user to share their availability
        /// </summary>
        AvailabilityShareRequest = 12,
        /// <summary>
        /// User is being notification of new pending events
        /// </summary>
        PendingEvents = 13,
        /// <summary>
        /// A user has synced and their contacts are being notified
        /// </summary>
        SyncComplete = 14,
        /// <summary>
        /// A contact has been invited to view a user's availability
        /// </summary>
        ContactInvite = 15,
        /// <summary>
        /// A user has been sent a summary of their contact's availability changes
        /// </summary>
        SummaryAlert = 16,
        /// <summary>
        /// A user has declined an offer
        /// </summary>
        OfferDeclined = 17,
        /// <summary>
        /// An offer has been withdrawn
        /// </summary>
        OfferWithdrawn = 18,
        /// <summary>
        /// A invitation sent to a hirer from an agency
        /// </summary>
        InviteHirer = 19
    }
}
