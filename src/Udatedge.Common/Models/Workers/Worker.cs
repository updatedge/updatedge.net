using System;
using System.Collections.Generic;

namespace Udatedge.Common.Models.Workers
{
    /// <summary>
    /// Describes a worker model
    /// </summary>
    public class Worker : BaseWorker
    {
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
        public DateTimeOffset LastShared { get; set; }
    }
}
