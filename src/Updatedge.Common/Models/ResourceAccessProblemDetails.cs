using System.Collections.Generic;
using Updatedge.Common.Extensions;

namespace Updatedge.Common.Models
{
    /// <summary>
    /// Problem details for issues with access to resources during request execution
    /// </summary>
    public class ResourceAccessProblemDetails : ApiProblemDetails
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="messages">Messages</param>
        public ResourceAccessProblemDetails(Dictionary<string, List<string>> messages)
        {
            SetProperties();
            Errors = messages;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="param">Paramter that failed</param>
        /// <param name="message">Erorr message</param>
        public ResourceAccessProblemDetails(string param, string message)
        {
            SetProperties();
            Errors = message.ToErrorsDictionary(param);
        }

        private void SetProperties()
        {
            Title = "Attempted access to forbidden resource(s).";
            Type = "https://apidocs.updatedge.com/docs/errors#forbidden-resource-access";
            Detail = "Access to some of the resources in your request were forbidden or the resources do not exist.";
            Status = 403;
        }
    }
}
