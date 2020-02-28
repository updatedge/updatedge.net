using System.Threading.Tasks;

namespace Updatedge.net.Services.V1
{
    public interface IInviteService
    {
        /// <summary>
        /// Invite a worker to share availability
        /// </summary>
        /// <param name="fromUserId">Id of the user to which the worker will share their availability.</param>
        /// <param name="toWorkerEmail">The email address of the worker to send the invitation to.</param>
        /// <returns>Invite Id</returns>        
        /// <exception cref="FlurlHttpException">Thrown if the https response is not in the 2xx range.</exception>
        Task<string> InviteWorkerAsync(string fromUserId, string toWorkerEmail);

        /// <summary>
        /// Invite a hirer to use your company as an agency
        /// </summary>
        /// <param name="fromUserId">The user at your organisation to be their contact</param>
        /// <param name="toHirerEmail">The hirer's email address to send the invitation to</param>
        /// <returns>Invite Id</returns>   
        /// <exception cref="FlurlHttpException">Thrown if the https response is not in the 2xx range.</exception>
        Task<string> InviteHirerAsync(string fromUserId, string toHirerEmail);
    }
}
