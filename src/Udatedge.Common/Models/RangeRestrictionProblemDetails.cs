using System.Collections.Generic;
using Udatedge.Common.Extensions;

namespace Udatedge.Common.Models
{
    /// <summary>
    /// Problem details for custom parameter validation failures
    /// </summary>
    public class RangeRestrictionProblemDetails : ApiProblemDetails
    {
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
