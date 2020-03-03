using System.Collections.Generic;

namespace Updatedge.Common.Validation
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
