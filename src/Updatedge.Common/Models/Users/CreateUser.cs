namespace Updatedge.Common.Models.Users
{
    /// <summary>
    /// Model to create a user within your organisation
    /// </summary>
    public class CreateUser
    {
        /// <summary>
        /// Email of the user to create
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// First name of the user to create
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the user to create
        /// </summary>
        public string LastName { get; set; }
    }
}
