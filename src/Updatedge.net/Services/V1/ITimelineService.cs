using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Udatedge.Common.Models.TimelineEvents;

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
        Task<List<TimelineEvent>> GetEventsAsync(string workerId, DateTimeOffset start, DateTimeOffset end);

        /// <summary>
        /// Gets a timeline event
        /// </summary>
        /// <param name="id">Id of the event</param>
        /// <returns>A specific timeline event.</returns>
        Task<List<TimelineEvent>> GetEventAsync(string id);

    }
}
