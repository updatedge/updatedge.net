using System;
using System.Collections.Generic;

namespace Updatedge.Common.Models.Workers
{
    /// <summary>
    /// Describes a worker model
    /// </summary>
    public class Worker : BaseWorker
    {

        public string ContactId { get; set; }

        /// <summary>
        /// Whether the worker has verified their identity.
        /// </summary>
        public bool Verified { get; set; }

        /// <summary>
        /// Emails associated with this worker.
        /// </summary>
        public List<string> Emails { get; set; }

        /// <summary>
        /// When this worker last shared.
        /// </summary>
        public DateTimeOffset? LastShared { get; set; }

        public List<WorkerAttribute> Attributes { get; set; }

        public List<Qualification> Qualifications { get; set; }

        public string Headline { get; set; }
        public bool ActivelyRepresenting { get; set; }
        public bool ActiveIssueRaised { get; set; }
        public bool ProfilePreviouslyApproved { get; set; }
        public bool ProfileCurrentlyRejected { get; set; }

        public bool Vetted { get; set; }
    }
}
