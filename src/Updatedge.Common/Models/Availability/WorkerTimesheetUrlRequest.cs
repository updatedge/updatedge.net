using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Updatedge.Common.Models.TimelineEvents;

namespace Updatedge.Common.Models.Availability
{
    public class WorkerTimesheetUrlRequest
    {
        /// <summary>
        /// The external id of the worker (if no internal id)
        /// </summary>
        public string WorkerExternalId { get; set; }

        /// <summary>
        /// The internal user id of the worker
        /// </summary>
        public string WorkerId { get; set; }

        /// <summary>
        /// The first name of the worker
        /// </summary>
        public string WorkerFirstName { get; set; }

        /// <summary>
        /// The last name of the worker
        /// </summary>
        public string WorkerLastName { get; set; }

        /// <summary>
        /// The email address of the worker (in case no UE account)
        /// This is used to ensure a contactId exists for any external worker
        /// </summary>
        public string WorkerEmail { get; set; }

        /// <summary>
        /// The fallback image url of the worker (in case no UE account)
        /// </summary>
        public string WorkerImageUrl { get; set; }

        /// <summary>
        /// Headline of the worker / the thing displayed under the name on timesheets
        /// </summary>
        public string WorkerHeadline { get; set; }

        /// <summary>
        /// total amount paid by all hirers combined
        /// </summary>
        public double TotalGrossPay { get; set; }

        /// <summary>
        /// Currency of gross pay
        /// </summary>
        public string Currency { get; set; }

        /// <summary> spent at all hirers
        /// Total Hours
        /// </summary>
        public double TotalHours { get; set; }

        /// <summary>
        /// The first day of the interval
        /// </summary>
        public DateTimeOffset? Start { get; set; }

        /// <summary>
        /// Number of days in the interval
        /// </summary>
        public int Days { get; set; }

        /// <summary>
        /// Internal Id of the agency
        /// </summary>
        public string AgencyId { get; set; }

        /// <summary>
        /// Name of the agency
        /// </summary>
        public string AgencyName { get; set; }

        /// <summary>
        /// Email of the agency
        /// </summary>
        public string AgencyEmail { get; set; }

        /// <summary>
        /// Hirers to present
        /// </summary>
        public ICollection<HirerDetails> Hirers { get; set; }

        /// <summary>
        /// Time period terminology
        /// </summary>
        public ICollection<TimesheetTerminology> Terminology { get; set; }

        public class HirerDetails
        {
            /// <summary>
            /// External id of the org
            /// </summary>
            public string OrganisationExternalId { get; set; }

            /// <summary>
            /// Primary Contact (e.g. Cover Manager) email address of the org
            /// (Can be used to create a contact for the org in UE if the org does not yet exist)
            /// </summary>
            public string PrimaryContactEmailAddress { get; set; }

            /// <summary>
            /// Other known contacts' email addresses within the org
            /// (Can be used to locate the org in UE)
            /// </summary>
            public ICollection<string> AuxiliaryEmailAddresses { get; set; }

            /// <summary>
            /// Postcode of org 
            /// (Can be used to locate the org in UE in conjuction with OrganisationName)
            /// </summary>
            public string OrganisationPostcode { get; set; }

            /// <summary>
            /// Internal id of the org
            /// </summary>
            public string OrganisationId { get; set; }

            /// <summary>
            /// Name of the org
            /// </summary>
            public string OrganisationName { get; set; }

            /// <summary>
            /// Domain of the org
            /// </summary>
            public string OrganisationDomain { get; set; }

            /// <summary>
            /// Source of metadata
            /// </summary>
            public string MetadataSource { get; set; }

            /// <summary>
            /// Urn
            /// </summary>
            public string Urn { get; set; }

            /// <summary>
            /// Total hours worked for this org
            /// </summary>
            public decimal TotalHours
            {
                get { return Events.Sum(span => span.Hours); }
            }

            /// <summary>
            /// Total gross pay from this org
            /// </summary>
            public double TotalGrossPay { get; set; }

            /// <summary>
            /// Events at this org
            /// </summary>
            public ICollection<PendingTimelineEvent> Events { get; set; }

            /// <summary>
            /// Rating (null indicates not yet rated)
            /// </summary>
            public int Rating { get; set; }

            /// <summary>
            /// Rating Timestamp (null indicates a brand new rating / value indicates historical rating)
            /// </summary>
            public DateTimeOffset? UpdatedAt { get; set; }
        }

        /// <summary>
        /// Event group summaries
        /// </summary>
        public ICollection<WorkerEventGroupResponse> Groups { get; set; }
    }
}
