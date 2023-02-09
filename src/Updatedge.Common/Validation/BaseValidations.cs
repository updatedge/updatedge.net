using System.Collections.Generic;
using System.Linq;

namespace Updatedge.Common.Validation
{
    /// <summary>
    /// Base Validator
    /// </summary>
    public class BaseValidations : IValidator
    {
        /// <summary>
        /// Readonly list of errors generated from validations.
        /// </summary>
        public Dictionary<string, List<string>> Messages { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public BaseValidations()
        {
            Messages = new Dictionary<string, List<string>>();
        }

        /// <summary>
        /// Adds a validation error
        /// </summary>
        /// <param name="paramName">Name of parameter</param>
        /// <param name="message">Validation message</param>
        protected void Add(string paramName, string message)
        {
            if (Messages.Any(v => v.Key.ToLower() == paramName.ToLower()))
            {
                Messages[paramName.ToLower()].Add(message);
            }
            else
            {
                Messages.Add(paramName.ToLower(), new List<string>() { { message } });
            }
        }



        /// <summary>
        /// Whether any validation errors exist
        /// </summary>
        public bool HasErrors => Messages.Any();
    }
}
