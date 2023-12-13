using System;
using System.Collections.Generic;
using System.Text;

namespace Updatedge.Common.Models.Rating
{
    public class SyncRatingsRequest
    {
        /// <summary>
        /// Agency's unique id for hirer
        /// </summary>
        public string ExternalHirerId { get; set; }

        /// <summary>
        /// Email address of the hirer within the hirer organisation
        /// </summary>
        public string HirerEmail { get; set; }

        /// <summary>
        /// The id of the agency that sent the timesheet data
        /// </summary>
        public string AgencyId { get; set; }

        /// <summary>
        /// Name of agency sending timesheet
        /// </summary>
        public string AgencyName { get; set; }

        /// <summary>
        /// List of workers with ratings as stored by agency
        /// </summary>
        public IEnumerable<ExternalWorkerRating> WorkerRatings { get; set; }

        /// <summary>
        /// List of workers with ratings *of this hirer* as stored by agency
        /// </summary>
        public IEnumerable<ExternalWorkerRating> HirerRatings { get; set; }

        /// <summary>
        /// Defines a worker rating relationship
        /// </summary>
        public class ExternalWorkerRating
        {
            /// <summary>
            /// User Id
            /// </summary>
            public string WorkerId { get; set; }

            /// <summary>
            /// Worker email address
            /// </summary>
            public string EmailAddress { get; set; }

            /// <summary>
            /// Rating
            /// </summary>
            public int Rating { get; set; }

            /// <summary>
            /// Rating Timestamp (null indicates a brand new rating)
            /// </summary>
            DateTimeOffset? UpdatedAt { get; set; }
        }
    }
}
