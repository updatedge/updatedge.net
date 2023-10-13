using System;
using System.Collections.Generic;
using Updatedge.Common.Models.Workers;

namespace Updatedge.Common.Models.Availability
{
    public class WorkersAvailabilityUrlRequest
    {
        /// <summary>
        /// Email to send timesheet response to
        /// </summary>
        public string AgencyContactEmail { get; set; }

        /// <summary>
        /// Agency contact full name
        /// </summary>
        public string AgencyContactFullName { get; set; }

        /// <summary>
        /// Agency's unique id for hirer
        /// </summary>
        public string ExternalHirerId { get; set; }

        /// <summary>
        /// Agency's name for hirer
        /// </summary>
        public string ExternalOrganisationName { get; set; }

        /// <summary>
        /// Web domain of organisation if known (used to retrieve logo)
        /// </summary>
        public string OrganisationDomain { get; set; }

        /// <summary>
        /// Start of period
        /// </summary>
        public DateTimeOffset Start { get; set; }

        /// <summary>
        /// Days in period
        /// </summary>
        public int Days { get; set; }

        /// <summary>
        /// Source of metadata
        /// </summary>
        public string MetadataSource { get; set; }

        /// <summary>
        /// Urn
        /// </summary>
        public string Urn { get; set; }

        /// <summary>
        /// The email address the timesheet will be sent to, to allow recipts to be sent.
        /// </summary>
        public string RecipientEmail { get; set; }

        /// <summary>
        /// Recipient's first name
        /// </summary>
        public string RecipientFirstName { get; set; }

        /// <summary>
        /// Recipient's last name
        /// </summary>
        public string RecipientLastName { get; set; }

        /// <summary>
        /// Workers
        /// </summary>
        public ICollection<WorkerRated> Workers { get; set; }

        /// <summary>
        /// Event group summaries
        /// </summary>
        public ICollection<WorkerEventGroupResponse> Groups { get; set; }

        /// <summary>
        /// Defines terminology for hour ranges
        /// </summary>
        public ICollection<TimesheetTerminology> Terminology { get; set; }
    }
}
