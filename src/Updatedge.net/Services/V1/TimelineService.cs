using Flurl;
using Flurl.Http;
using Light.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Updatedge.Common.Models.TimelineEvents;
using Updatedge.Common.Validation;
using Updatedge.net.Configuration;
using Updatedge.net.Exceptions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Updatedge.net.Services.V1
{
    /// <summary>
    /// Timeline service API wrapper
    /// </summary>
    public class TimelineService : BaseService, ITimelineService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="config">Updatedge configuration setttings</param>
        public TimelineService(IUpdatedgeConfiguration config) : base(config)
        {
        }
                
        public virtual async Task<List<TimelineEvent>> GetEventsAsync(string workerId, DateTimeOffset start, DateTimeOffset end)
        {
            try
            {
                // VALIDATION ------------------------------

                var validator = new RequestValidator(
                    new IntervalValidations(start, end)
                        .StartEndSpecified()
                        .LessThanXDays(32)
                        .EndsAfterStart()
                    );

                if (validator.HasErrors) throw new ApiWrapperException(validator.ToDetails());

                var response = await BaseUrl
                    .AppendPathSegment($"timeline/{workerId}")
                    .SetQueryParam("api-version", ApiVersion)
                    .SetQueryParam("start", start.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"))
                    .SetQueryParam("end", end.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"))                    
                    .WithHeader(ApiKeyName, ApiKey)
                    .GetAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                {   
                    return new List<TimelineEvent>();
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<TimelineEvent>>(responseContent, JsonOptions);
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }

        public virtual async Task<List<TimelineEvent>> GetEventAsync(string id)           
        {
            try 
            {
                // check that we have an Event Id.
                id.MustNotBeNullOrEmpty("Please provide an Event Id.");
                var response = await BaseUrl
                    .AppendPathSegment($"timeline/event/{id}")
                    .SetQueryParam("api-version", ApiVersion)
                    .WithHeader(ApiKeyName, ApiKey)
                    .GetAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return new List<TimelineEvent>();
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<TimelineEvent>>(responseContent, JsonOptions);
                                
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }

        /// <summary>
        /// Requests permission to add a set of working events to a worker's timeline.
        /// </summary>
        /// <param name="userId">Id of user submitting request</param>
        /// <param name="pendingEvents">Events to be added</param>
        /// <returns>The new pending event set id</returns>
        public virtual async Task<string> CreatePendingWorkingEventsAsync(string userId,
            List<PendingTimelineEvent> pendingEvents)           
        {
            try 
            {
                // check that we have an user id.
                userId.MustNotBeNullOrEmpty("Please provide a sending user id.");

                // check we have pending events.
                pendingEvents.MustNotBeNull("Please provide a set of pending events.");

                var response = await BaseUrl
                    .AppendPathSegment($"timeline/pendingWorkEvents")
                    .SetQueryParam("api-version", ApiVersion)
                    .SetQueryParam("userId", userId)
                    .WithHeader(ApiKeyName, ApiKey)
                    .PostJsonAsync(pendingEvents);

                var responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;

            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }
    }
}
