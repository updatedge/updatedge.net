using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Udatedge.Common.Models.Availability;

namespace Updatedge.net.Services.V1
{
    public interface IAvailabilityService
    {
        /// <summary>
        /// Get daily availability for given workers
        /// </summary>
        /// <param name="workerIds">An array of worker ids about which you wish to retrieve availability.</param>
        /// <param name="start">Start date and time (UTC).</param>
        /// <param name="end">End date and time (UTC).</param>
        /// <param name="daysToRepeat">Number of days to repeat the interval across (minimum: 0, maximum: 31, default: 0).</param>
        /// <returns>A list of availability</returns>
        /// <exception cref="FlurlHttpException">Thrown if the https response is not in the 2xx range.</exception>
        Task<List<WorkerAvailabilityIntervals>> GetAvailabilityDailyAsync
            (DateTimeOffset start, DateTimeOffset end, int daysToRepeat, IEnumerable<string> workerIds);

        /// <summary>
        /// Get overall availability
        /// </summary>
        /// <param name="request">The workers and intervals to query across</param>
        /// <returns>A percentage value for each worker specified.</returns>
        /// <exception cref="FlurlHttpException">Thrown if the https response is not in the 2xx range.</exception>
        Task<List<WorkerOverallAvailability>> GetTotalAvailability(WorkersIntervalsRequest request);
    }
}
