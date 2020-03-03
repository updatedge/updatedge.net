using System.Collections.Generic;
using System.Threading.Tasks;
using Udatedge.Common.Models.Users;

namespace Updatedge.net.Services.V1
{
    public interface IUserService
    {
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>list of user for company.</returns>
        /// <exception cref="FlurlHttpException">Thrown if the https response is not in the 2xx range.</exception>
        Task<IEnumerable<User>> GetAllAsync();

        /// <summary>
        /// Get a user by Id
        /// </summary>
        /// <param name="id">Id of user</param>
        /// <returns>User</returns>
        /// <exception cref="FlurlHttpException">Thrown if the https response is not in the 2xx range.</exception>
        Task<User> GetByIdAsync(string id);

        /// <summary>
        /// Get a user my email
        /// </summary>
        /// <param name="email">user's email</param>
        /// <returns>User</returns>
        /// <exception cref="FlurlHttpException">Thrown if the https response is not in the 2xx range.</exception>
        Task<User> GetByEmailAsync(string email);

        /// <summary>
        /// Create a user
        /// </summary>
        /// <param name="user">Details of user to create</param>
        /// <returns>New user Id</returns>
        /// <exception cref="FlurlHttpException">Thrown if the https response is not in the 2xx range.</exception>
        Task<string> CreateUserAsync(CreateUser user);

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="user">Details of user update</param>
        /// <returns>True if update is as success</returns>
        /// <exception cref="FlurlHttpException">Thrown if the https response is not in the 2xx range.</exception>
        Task<bool> UpdateUserAsync(User user);

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id">Id of user to be deleted</param>
        /// <returns>True if delete is a success</returns>
        /// <exception cref="FlurlHttpException">Thrown if the https response is not in the 2xx range.</exception>
        Task<bool> DeleteUserAsync(string id);


    }
}
