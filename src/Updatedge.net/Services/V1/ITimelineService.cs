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
    }
}
