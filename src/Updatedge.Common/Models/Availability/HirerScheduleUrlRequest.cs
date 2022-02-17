using System;
using System.Collections.Generic;
using System.Text;

namespace Updatedge.Common.Models.Availability
{
    public class HirerScheduleUrlRequest
    {
        /// <summary>
        /// Agency's unique id for hirer
        /// </summary>
        public string ExternalHirerId { get; set; }

        /// <summary>
        /// Urn of hirer
        /// </summary>
        public string Urn { get; set; }

        /// <summary>
        /// Id of the agency that is requesting the url
        /// </summary>
        public string AgencyId { get; set; }

        /// <summary>
        /// Name of the agency that is requesting the url
        /// </summary>
        public string AgencyName { get; set; }

        /// <summary>
        /// List of workers with ratings as stored by agency
        /// </summary>
        public IEnumerable<WorkerRating> WorkerRatings { get; set; }

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

            /// <summary>
            /// First name
            /// </summary>
            public string FirstName { get; set; }

            /// <summary>
            /// Last name
            /// </summary>
            public string LastName { get; set; }
        }
    }
}
