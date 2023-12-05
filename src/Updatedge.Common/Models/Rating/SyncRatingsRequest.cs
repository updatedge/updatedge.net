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
        public IEnumerable<WorkerRating> WorkerRatings { get; set; }

        /// <summary>
        /// List of workers with ratings *of this hirer* as stored by agency
        /// </summary>
        public IEnumerable<WorkerRating> HirerRatings { get; set; }

        /// <summary>
        /// Defines a worker rating relationship
        /// </summary>
        public class WorkerRating
        {
            /// <summary>
            /// User Id
            /// </summary>
            public string WorkerId { get; set; }

            /// <summary>
            /// Rating
            /// </summary>
            public int Rating { get; set; }
        }
    }
}
