using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Updatedge.Common.Models.TimelineEvents;

namespace Updatedge.net.Services.V1
{
    public interface ITimelineService
    {
        /// <summary>
        /// Gets a worker's events
        /// </summary>
        /// <param name="workerId">Id of worker</param>
        /// <param name="start">start of period</param>
        /// <param name="end">end of period</param>
        /// <returns>A a list of timeline events.</returns>        
        /// <exception cref="FlurlHttpException">Thrown if the https response is not in the 2xx range.</exception>
        Task<List<TimelineEvent>> GetEventsAsync(string workerId, DateTimeOffset start, DateTimeOffset end);

        /// <summary>
        /// Gets a timeline event
        /// </summary>
        /// <param name="id">Id of the event</param>
        /// <returns>A specific timeline event.</returns>
        /// <exception cref="FlurlHttpException">Thrown if the https response is not in the 2xx range.</exception>
        Task<List<TimelineEvent>> GetEventAsync(string id);

        /// <summary>
        /// Requests permission to add a set of working events to a worker's timeline.
        /// </summary>
        /// <param name="userId">Id of user submitting request</param>
        /// <param name="pendingEvents">Events to be added</param>
        /// <returns>The new pending event set id</returns>
        Task<string> CreatePendingWorkingEventsAsync(string userId, List<PendingTimelineEvent> pendingEvents);
        
        /// <summary>
        /// Retrieves the furthest point in time at which each user specified becomes available (when their inferred availability ends).   
        /// </summary>
        /// <param name="userIds">List of userids to return max availability for</param>
        /// <returns>Max availability for each user id</returns>
        Task<List<WorkerMaxAvailability>> GetMaxAvailabilityDatesAsync(List<string> userIds);

        /// <summary>
        /// Retrieves when the last shared date and time for a given set of users.
        /// </summary>
        /// <param name="userIds">List of user ids</param>
        /// <returns>Last shared date/times for each user (or no entry if never shared)</returns>
        Task<List<LastSharedDetail>> GetLastSharedOn(List<string> userIds);

        /// <summary>
        /// Gets a worker's events with overlaps merged and flattened
        /// </summary>
        /// <param name="workerId">Id of worker</param>
        /// <param name="start">start of period</param>
        /// <param name="end">end of period</param>
        /// <param name="includeInferredBusy">whether to include inferred busy periods in the result set along with worker events</param>
        /// <returns>A a list of timeline events.</returns>        
        Task<List<FlattenedTimelineEvent>> GetFlattenedEventsAsync(string workerId, DateTimeOffset start, DateTimeOffset end,
            bool includeInferredBusy = false);

        // <summary>
        /// Retrieves a list of inferred availability periods for one or more users  
        /// </summary>
        /// <param name="userIds">List of userids to return max availability for</param>
        /// <returns>Inferred availabilities for each user id</returns>
        Task<List<WorkerInferredUnavailabilities>> GetInferredAvailabilityInTimeframeAsync(List<string> userIds, DateTimeOffset start, DateTimeOffset end);
    }
}
