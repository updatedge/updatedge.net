using System.Collections.Generic;
using System.Linq;
using Updatedge.Common.Models;

namespace Updatedge.Common.Validation
{
    /// <summary>
    /// Stores the results of a number of validations.
    /// </summary>
    public class RequestValidator
    {
        /// <summary>
        /// Internal validator list
        /// </summary>
        private List<IValidator> _validators = new List<IValidator>();

        /// <summary>
        /// List of validators
        /// </summary>
        public List<IValidator> Validators => _validators;

        /// <summary>
        /// Constructor that adds multiple validators
        /// </summary>
        /// <param name="validator"></param>
        public RequestValidator(IValidator validator)
        {
            Validators.Add(validator);
        }

        /// <summary>
        /// Adds an unspecified number of validators
        /// </summary>
        /// <param name="validators">Validators</param>
        public RequestValidator(params IValidator[] validators)
        {
            foreach (var validator in validators)
            {
                Validators.Add(validator);
            }
        }

        /// <summary>
        /// Returns when any validator error messages have been created.
        /// </summary>
        public bool HasErrors
        {
            get
            {
                foreach (var val in _validators)
                {
                    if (val.Messages.Any()) return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Wraps all problems with a custom class
        /// </summary>
        public RangeRestrictionProblemDetails ToDetails()
        {
            var results = new Dictionary<string, List<string>>();

            foreach (var validator in Validators)
            {
                foreach (var key in validator.Messages.Keys)
                {
                    results.Add(key, validator.Messages[key]);
                }
            }

            return new RangeRestrictionProblemDetails(results);
        }

    }
}
