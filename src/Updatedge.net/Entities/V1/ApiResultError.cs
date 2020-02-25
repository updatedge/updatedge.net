using System.Collections.Generic;

namespace Updatedge.net.Entities.V1
{
    /// <summary>
    /// An error with the API request
    /// </summary>
    public class ApiResultError
    {
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Error type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Detailed error description
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Items related to this error
        /// </summary>
        public Dictionary<string, List<string>> Errors { get; set; }

    }
}
