using System;
using System.Collections.Generic;
using Updatedge.Common.Enumerations;

namespace Updatedge.Common
{
    public static class Constants
    {
        public static class Headers
        {
            public const string ApiKey = "X-UE-Api-Subscription-Key";
        }

        public static class ErrorMessages
        {
            public const string UnauthorisedIdsDescription = "You may not be authorised to query those resources or they don't exist.";
            public const string UnauthorisedIdDescription = "You may not be authorised to access the resource or it doesn't exist.";
            public const string UserExistingEmail = "A user with that email address already exists.";
            public const string DomainMismatch = "The given email address must match your company domain.";
            public const string NoWorkerIdsSpecified = "You must supply at least one worker id.";
            public const string NoIntervalsSpecified = "You must supply at least one interval.";
            public const string XMustBeBeforeY = "{0} must be before {1}.";
            public const string XMustBeWithinYHoursOfZ = "{0} must be within {1} hours of {2} (inclusive).";
            public const string XMustBeWithinYDaysOfZ = "{0} must be within {1} days of {2} (inclusive).";
            public const string XMustBeBetweenYAndZ = "Value must be between {0} and {1} (inclusive).";
            public const string NotDefaultDateTimeOffset = "{0} must be specified.";
            public const string IntervalsOverlap = "At least one of the specified intervals overlap.";
            public const string ValueNotSpecified = "Value must be specified or parameter name incorrectly formed.";
            public const string MustStartInFuture = "{0} must be in the future.";
            public const string EmailInvalid = "Value is not a valid email address";
            public const string OfferWorkersInvalid = "One or more of the ids are invalid. All ids must be workers who have applied for the offer or it will not be marked as completed.";
            public const string OfferAlreadyComplete = "Offer has already been completed";
            public const string NudgeLimitReached = "The nudge limit has been reached for this worker";
        }

        public static class QueryStringParamNames
        {
            public const string ApiVersion = "api-version";
        }

        public static class RequestScopeItems
        {
            public const string RequestId = "X-UE-Request-ID";
        }

        public static class QueueSettings
        {
            public const string WorkItemQueueName = "workitems";
        }

        public static class WorkItemTypes
        {
            public const int PushGeneralMessage = 5;
            public const int Push = 7;
            public const int WorkerInvite = 9;
            public const int Email = 10;
        }

        public static class Notifications
        {
            public static readonly Dictionary<NotificationReason, (int count, TimeSpan period)> Limits = new Dictionary<NotificationReason, (int count, TimeSpan period)>
            {
                // Only allow 2 nudges within a 24 hour period
                { NotificationReason.ContactNudge, (2, TimeSpan.FromHours(24)) },
                
                // Only allow 2 password reset requests an hour
                { NotificationReason.RequestPasswordReset, (2, TimeSpan.FromHours(1)) }
            };
        }
    }
}
