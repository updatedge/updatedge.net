using Light.GuardClauses;
using Updatedge.Common;

namespace Updatedge.Common.Validation
{
    /// <summary>
    /// General string validations
    /// </summary>
    public class StringValidation : BaseValidations
    {
        private readonly string _value;
        private readonly string _paramName;

        /// <summary>
        /// Constructor
        /// </summary>
        public StringValidation(string value, string paramName) : base()
        {
            _value = value;
            _paramName = paramName;
        }

        /// <summary>
        /// Validates a string is present
        /// </summary>
        /// <returns></returns>
        public StringValidation IsNotNullOrEmpty()
        {
            if (!string.IsNullOrEmpty(_value)) return this;

            Add(_paramName, Constants.ErrorMessages.ValueNotSpecified);
            return this;
        }

        /// <summary>
        /// Validates an email address
        /// </summary>
        /// <returns></returns>
        public StringValidation IsEmail()
        {
            if (RegularExpressions.EmailRegex.IsMatch(_value)) return this;

            Add(_paramName, Constants.ErrorMessages.EmailInvalid);
            return this;

        }
    }
}
