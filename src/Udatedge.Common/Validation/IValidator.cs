using System.Collections.Generic;

namespace Udatedge.Common.Validation
{
    /// <summary>
    /// Validator interface
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// List of validation messages
        /// </summary>
        Dictionary<string, List<string>> Messages { get; }
    }
}
