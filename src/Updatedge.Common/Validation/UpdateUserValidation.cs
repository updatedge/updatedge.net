using Light.GuardClauses;
using Updatedge.Common.Models.Users;

namespace Updatedge.Common.Validation
{
    /// <summary>
    /// Validates an UpdateUser request.
    /// </summary>
    public class UpdateUserValidation : BaseValidations
    {
        private readonly User _user;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="user">User to be validated</param>
        public UpdateUserValidation(User user) : base()
        {
            _user = user;
        }

        /// <summary>
        /// Ensure id not null or empty
        /// </summary>
        /// <returns>Itself</returns>
        public UpdateUserValidation IdNotNullOrEmptyAndCorrectFormat()
        {
            if (string.IsNullOrEmpty(_user.Id))
                Add(nameof(_user.Id), Constants.ErrorMessages.ValueNotSpecified);

            return this;
        }

        /// <summary>
        /// Ensure email not null or empty and correct format
        /// </summary>
        /// <returns>Itself</returns>
        public UpdateUserValidation EmailNotNullOrEmptyAndCorrectFormat()
        {
            if (string.IsNullOrEmpty(_user.Email))
                Add(nameof(_user.Email), Constants.ErrorMessages.ValueNotSpecified);

            if (!RegularExpressions.EmailRegex.Match(_user.Email).Success)
                Add(nameof(_user.Email), Constants.ErrorMessages.EmailInvalid);

            return this;
        }

        /// <summary>
        /// Ensure FirstName not null or empty
        /// </summary>
        /// <returns>Itself</returns>
        public UpdateUserValidation FirstNameNotNullOrEmpty()
        {
            if (string.IsNullOrEmpty(_user.FirstName))
                Add(nameof(_user.FirstName), Constants.ErrorMessages.ValueNotSpecified);

            return this;
        }

        /// <summary>
        /// Ensure LastName not null or empty
        /// </summary>
        /// <returns>Itself</returns>
        public UpdateUserValidation LastNameNotNullOrEmpty()
        {
            if (string.IsNullOrEmpty(_user.LastName))
                Add(nameof(_user.LastName), Constants.ErrorMessages.ValueNotSpecified);

            return this;
        }

    }
}
