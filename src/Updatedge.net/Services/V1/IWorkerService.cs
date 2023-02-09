using System.Threading.Tasks;
using Updatedge.Common.Models.Workers;

namespace Updatedge.net.Services.V1
{
    public interface IWorkerService
    {
        /// <summary>
        /// Get a worker by id
        /// </summary>
        /// <param name="id">The id of the worker.</param>
        /// <returns>Worker</returns>
        /// <exception cref="FlurlHttpException">Thrown if the https response is not in the 2xx range.</exception>
        Task<Worker> GetWorkerByIdAsync(string id);

        /// <summary>
        /// Get a worker by email
        /// </summary>
        /// <param name="email">Email address to search for.</param>
        /// <returns>Worker</returns>
        /// <exception cref="FlurlHttpException">Thrown if the https response is not in the 2xx range.</exception>
        Task<BaseWorker> GetWorkerByEmailAsync(string email);

        /// <summary>
        /// Nudge a worker to share their availability
        /// </summary>
        /// <param name="fromUserId">ID of the user sending the nudge</param>
        /// <param name="toWorkerId">ID of the worker receiving the nudge</param>
        /// <returns></returns>
        /// <exception cref="FlurlHttpException">Thrown if the https response is not in the 2xx range.</exception>
        Task<bool> NudgeWorkerAsync(string fromUserId, string toWorkerId);

    }
}
