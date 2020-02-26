using System.Collections.Generic;
using Udatedge.Common.Extensions;

namespace Udatedge.Common.Models
{
    /// <summary>
    /// Problem details for custom parameter validation failures
    /// </summary>
    public class RangeRestrictionProblemDetails
    {
        /// <summary>
        /// Short summary of the problem type.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// URI reference to a description of the problem type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// More descriptive explanation of the problem type.
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// HTTP status code
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// Parameters and error messages
        /// </summary>
        public IDictionary<string, List<string>> Errors { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="messages">Messages</param>
        public RangeRestrictionProblemDetails(Dictionary<string, List<string>> messages)
        {
            SetProperties();
            Errors = messages;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="param">Paramter that failed.</param>
        /// <param name="message">Error message.</param>
        public RangeRestrictionProblemDetails(string param, string message)
        {
            SetProperties();
            Errors = message.ToErrorsDictionary(param);
        }

        private void SetProperties()
        {
            Title = "Parameter(s) out of range.";
            Type = "https://apidocs.updatedge.com/docs/errors#parameters-out-of-range";
            Detail = "Querystring or Body values were submitted which did not fall into their accepted range or validation requirements.";
            Status = 400;

        }
    }
}
