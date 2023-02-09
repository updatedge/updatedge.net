namespace Updatedge.Common.Models.Users
{
    /// <summary>
    /// Base User
    /// </summary>
    public class BaseUser
    {
        /// <summary>
        /// The user's ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The user's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The user's last name
        /// </summary>
        public string LastName { get; set; }

    }

    /// <summary>
    /// Model of a user within your organisation
    /// </summary>
    public class User : BaseUser
    {
        /// <summary>
        /// Whether the user has been soft-deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// The user's email
        /// </summary>
        public string Email { get; set; }
    }
}
