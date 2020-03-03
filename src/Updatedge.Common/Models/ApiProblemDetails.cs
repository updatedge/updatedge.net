using System.Collections.Generic;

namespace Updatedge.Common.Models
{
    /// <summary>
    /// Api related problem details object
    /// </summary>
    public class ApiProblemDetails : BaseApiProblemDetails
    {   /// <summary>
        /// Parameters and error messages
        /// </summary>
        public IDictionary<string, List<string>> Errors { get; set; }
    }
}
