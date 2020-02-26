using Light.GuardClauses;
using Udatedge.Common;
using Udatedge.Common.Models.Users;
using Udatedge.Common.Validation;

namespace Updatedge.PlatformAPI.Controllers.V1.Validation
{
    /// <summary>
    /// Validates a CreateUser request.
    /// </summary>
    public class CreateUserValidation : BaseValidations
    {
        private readonly CreateUser _user;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="user">User to be validated</param>
        public CreateUserValidation(CreateUser user)
        {
            _user = user;
        }

        /// <summary>
        /// Ensure email not null or empty and correct format
        /// </summary>
        /// <returns>Itself</returns>
        public CreateUserValidation EmailNotNullOrEmptyAndCorrectFormat()
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
        public CreateUserValidation FirstNameNotNullOrEmpty()
        {
            if (string.IsNullOrEmpty(_user.FirstName))
                Add(nameof(_user.FirstName), Constants.ErrorMessages.ValueNotSpecified);

            return this;
        }

        /// <summary>
        /// Ensure LastName not null or empty
        /// </summary>
        /// <returns>Itself</returns>
        public CreateUserValidation LastNameNotNullOrEmpty()
        {
            if (string.IsNullOrEmpty(_user.LastName))
                Add(nameof(_user.LastName), Constants.ErrorMessages.ValueNotSpecified);

            return this;
        }

    }
}
