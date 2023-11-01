using Flurl;
using Flurl.Http;
using Light.GuardClauses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
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

        public virtual async Task<List<FlattenedTimelineEvent>> GetFlattenedEventsAsync(string workerId, DateTimeOffset start, DateTimeOffset end,
            bool includeInferredBusy = false)
        {
            try
            {
                // VALIDATION ------------------------------

                var validator = new RequestValidator(
                    new IntervalValidations(start, end)
                        .StartEndSpecified()
                        .LessThanXDays(90)
                        .EndsAfterStart()
                );

                if (validator.HasErrors) throw new ApiWrapperException(validator.ToDetails());

                var response = await BaseUrl
                    .AppendPathSegment($"timeline/flattened/{workerId}")
                    .SetQueryParam("api-version", ApiVersion)
                    .SetQueryParam("start", start.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"))
                    .SetQueryParam("end", end.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"))
                    .SetQueryParam("includeinferredbusy", includeInferredBusy)
                    .WithHeader(ApiKeyName, ApiKey)
                    .GetAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return new List<FlattenedTimelineEvent>();

                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<FlattenedTimelineEvent>>(responseContent, JsonOptions);
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }

        public virtual async Task<List<TimelineEvent>> GetEventsAsync(string workerId, DateTimeOffset start, DateTimeOffset end)
        {
            try
            {
                // VALIDATION ------------------------------

                var validator = new RequestValidator(
                    new IntervalValidations(start, end)
                        .StartEndSpecified()
                        .LessThanXDays(90)
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

        // <summary>
        /// Retrieves the furthest point in time at which each user specified becomes available (when their inferred availability ends).   
        /// </summary>
        /// <param name="userIds">List of userids to return max availability for</param>
        /// <returns>Max availability for each user id</returns>
        public async Task<List<WorkerMaxAvailability>> GetMaxAvailabilityDatesAsync(List<string> userIds)
        {
            try
            {
                userIds.MustNotBeNullOrEmpty("Please provide one or more user ids.");

                var response = await BaseUrl
                    .AppendPathSegment($"timeline/maxAvailabilityDates")
                    .SetQueryParam("api-version", ApiVersion)
                    .WithHeader(ApiKeyName, ApiKey)
                    .PostJsonAsync(userIds);

                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<WorkerMaxAvailability>>(responseContent, JsonOptions);
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }

        }

        /// <summary>
        /// Retrieves when the last shared date and time for a given set of users.
        /// </summary>
        /// <param name="userIds">List of user ids</param>
        /// <returns>Last shared date/times for each user (or no entry if never shared)</returns>
        public async Task<List<LastSharedDetail>> GetLastSharedOn(List<string> userIds)
        {
            try
            {
                userIds.MustNotBeNullOrEmpty("Please provide one or more user ids.");

                var response = await BaseUrl
                    .AppendPathSegment($"timeline/lastSharedOn")
                    .SetQueryParam("api-version", ApiVersion)
                    .WithHeader(ApiKeyName, ApiKey)
                    .PostJsonAsync(userIds);

                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<LastSharedDetail>>(responseContent, JsonOptions);
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }
    }
}
